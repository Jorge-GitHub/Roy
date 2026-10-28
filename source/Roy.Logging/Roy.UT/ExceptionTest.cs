// Default run: dotnet test --filter TestCategory!=Live
// The Live tests send email through smtp.ethereal.email, post to public APIs, or write to
// the Windows Application event log. Run them by hand when those paths change.
using Roy.Logging;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.DTO.Communication;
using Roy.Logging.Domain.Settings;
using Roy.Logging.Domain.Settings.Database;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using Roy.UT.Entities;
using System.Diagnostics;
using System.Text.Json;

namespace Roy.UT;

/// <summary>
/// Exception tests.
/// </summary>
[TestClass]
public class ExceptionTest
{
    /// <summary>
    /// This test's helper and folder.
    /// </summary>
    private UTHelper Helper { get; set; }

    /// <summary>
    /// Fresh settings and an empty folder for every test.
    /// </summary>
    [TestInitialize]
    public void Initialize()
    {
        this.Helper = new UTHelper();
        this.Helper.ResetSettings();
    }

    /// <summary>
    /// Delete the test's files.
    /// </summary>
    [TestCleanup]
    public void Cleanup()
    {
        this.Helper.DeleteFiles();
    }

    /// <summary>
    /// Test saving the exception.
    /// </summary>
    [TestMethod]
    public async Task TestException()
    {
        ProcessMessageDTO result = await new Exception("Test Exception")
            .SaveAsync(new StackFrame(0, true));

        this.Helper.AssertNoErrors(result);
        Assert.IsTrue(result.Succeed);
        string file = this.Helper.GetSingleFile(this.Helper.ExceptionsFolder);
        StringAssert.StartsWith(Path.GetFileName(file), "Error.");
        JsonElement json = this.Helper.ReadJson(file);
        Assert.AreEqual("Test Exception", json.GetProperty("ExceptionMessage").GetString());
        Assert.AreEqual((int)Level.Error, json.GetProperty("Level").GetInt32());
        JsonElement frame = json.GetProperty("StackFrame");
        StringAssert.EndsWith(frame.GetProperty("CallerFileName").GetString(), "ExceptionTest.cs");
        Assert.IsTrue(frame.GetProperty("CallerLineNumber").GetInt32() > 0);
    }

    /// <summary>
    /// Test that setting the file name will work.
    /// </summary>
    [TestMethod]
    public async Task TestFileNameException()
    {
        LogExtension.Settings.Exception.FileName = "test.txt";
        string fileLocation = this.Helper.GetfullPathToFile(
            this.Helper.ExceptionsFolder, LogExtension.Settings.Exception.FileName);

        await new Exception("Test file Name Exception").SaveAsync();

        Assert.IsTrue(File.Exists(fileLocation));
        Assert.AreEqual("Test file Name Exception",
            this.Helper.ReadJson(fileLocation).GetProperty("ExceptionMessage").GetString());
    }

    /// <summary>
    /// Test that a sink that fails is reported in the result.
    /// </summary>
    [TestMethod]
    public async Task TestSaveErrorIsReported()
    {
        // A file where the folder should be, so the folder cannot be created.
        Directory.CreateDirectory(this.Helper.Root);
        string blocker = Path.Combine(this.Helper.Root, "blocker");
        File.WriteAllText(blocker, string.Empty);
        LogExtension.Settings.Exception.FolderLocation = blocker;

        ProcessMessageDTO result = await new Exception("Test Exception").SaveAsync();

        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsFalse(result.Succeed);
    }

    /// <summary>
    /// Test that disabling the exceptions saves nothing and fails nothing.
    /// </summary>
    [TestMethod]
    public async Task TestExceptionDisabled()
    {
        LogExtension.Settings.Exception.Disable = true;

        ProcessMessageDTO result = await new Exception("Test Exception").SaveAsync();

        Assert.IsTrue(result.Succeed);
        Assert.IsFalse(Directory.Exists(this.Helper.ExceptionsFolder));
    }

    /// <summary>
    /// Test that disabling the logs does not disable the exceptions.
    /// </summary>
    [TestMethod]
    public async Task TestLogDisableDoesNotStopExceptions()
    {
        LogExtension.Settings.Log.Disable = true;

        ProcessMessageDTO result = await new Exception("Test Exception").SaveAsync();

        Assert.IsTrue(result.Succeed);
        this.Helper.GetSingleFile(this.Helper.ExceptionsFolder);
    }

    /// <summary>
    /// Test that a database set to other levels is left alone.
    /// </summary>
    [TestMethod]
    public async Task TestDatabaseLevelFilter()
    {
        DatabaseSetting setting = this.Helper.GetOfflineDatabaseSetting();
        setting.LevelsToSave.Add(Level.Warning);
        LogExtension.Settings.Exception.Databases.Add(setting);
        LogExtension.Settings.Exception.SaveLogOnFile = false;

        // Level is Error, the setting saves Warning only: no connection is attempted,
        // so the bad connection string raises no error.
        ProcessMessageDTO result = await new Exception("Test Exception").SaveAsync();

        this.Helper.AssertNoErrors(result);
    }

    /// <summary>
    /// Test that a database that cannot be reached is reported in the result.
    /// </summary>
    [TestMethod]
    public async Task TestDatabaseFailureIsReported()
    {
        LogExtension.Settings.Exception.Databases.Add(
            this.Helper.GetOfflineDatabaseSetting());
        LogExtension.Settings.Exception.SaveLogOnFile = false;

        ProcessMessageDTO result = await new Exception("Test Exception").SaveAsync();

        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsFalse(result.Succeed);
    }

    /// <summary>
    /// Test that an exception record at the default level says where it was thrown.
    /// </summary>
    [TestMethod]
    public async Task TestStackTraceAtErrorLevel()
    {
        ProcessMessageDTO result = await this.Helper.ThrowAndCatch().SaveAsync();

        this.Helper.AssertNoErrors(result);
        JsonElement json = this.Helper.ReadJson(
            this.Helper.GetSingleFile(this.Helper.ExceptionsFolder));
        StringAssert.Contains(json.GetProperty("StackTrace").GetString(), "ThrowAndCatch");
        Assert.IsFalse(json.TryGetProperty("ExceptionTrace", out _));
    }

    /// <summary>
    /// Test that Debug adds the full exception on top of the stack trace.
    /// </summary>
    [TestMethod]
    public async Task TestFullExceptionAtDebugLevel()
    {
        ProcessMessageDTO result = await this.Helper.ThrowAndCatch().SaveAsync(Level.Debug);

        this.Helper.AssertNoErrors(result);
        JsonElement json = this.Helper.ReadJson(
            this.Helper.GetSingleFile(this.Helper.ExceptionsFolder));
        StringAssert.Contains(json.GetProperty("StackTrace").GetString(), "ThrowAndCatch");
        Assert.IsTrue(json.TryGetProperty("ExceptionTrace", out _));
    }

    /// <summary>
    /// Test that an API that cannot be reached is reported in the result.
    /// Before the post was awaited, the failure was lost.
    /// </summary>
    [TestMethod]
    public async Task TestAPIFailureIsReported()
    {
        LogExtension.Settings.Exception.APIs.Add(this.Helper.GetOfflineAPISetting());
        LogExtension.Settings.Exception.SaveLogOnFile = false;

        ProcessMessageDTO result = await new Exception("Test Exception").SaveAsync();

        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsInstanceOfType(result.Errors[0], typeof(HttpRequestException));
    }

    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod, TestCategory("Live")]
    public async Task TestEmailException()
    {
        LogExtension.Settings.Exception.Emails.Add(this.Helper.GetEmailSetting());

        ProcessMessageDTO result = await new Exception("Test Exception")
            .SaveAsync(Level.Emergency, new StackFrame(0, true));

        this.Helper.AssertNoErrors(result);
    }

    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod, TestCategory("Live")]
    public async Task TestEmailExceptionFrenchVersion()
    {
        LogExtension.Settings.Exception.Emails.Add(this.Helper.GetEmailSetting());
        LogExtension.Settings.Exception.Emails[0].Language = Language.French;

        ProcessMessageDTO result = await new Exception("Test Exception")
            .SaveAsync(Level.Emergency, new StackFrame(0, true));

        this.Helper.AssertNoErrors(result);
    }

    /// <summary>
    /// Test that a particular level error will not be send to the user.
    /// </summary>
    [TestMethod]
    public async Task TestEmailExceptionNotSendingEmailToLevel()
    {
        EmailSetting setting = this.Helper.GetOfflineEmailSetting();
        setting.LevelsToReport.Add(Level.Warning);
        LogExtension.Settings.Exception.Emails.Add(setting);
        LogExtension.Settings.Exception.SaveLogOnFile = false;

        // This exception will not be send to the user because it is not an error
        // but a Warning. The default level log is Error. The server is unreachable,
        // so an attempt to send would show up as an error.
        ProcessMessageDTO result = await new Exception("Test Warning Not Send").SaveAsync();

        this.Helper.AssertNoErrors(result);
    }

    /// <summary>
    /// Test that an email that cannot be sent is reported in the result.
    /// </summary>
    [TestMethod]
    public async Task TestEmailExceptionSendingFailureIsReported()
    {
        LogExtension.Settings.Exception.Emails.Add(this.Helper.GetOfflineEmailSetting());
        LogExtension.Settings.Exception.SaveLogOnFile = false;

        ProcessMessageDTO result = await new Exception("Test Exception").SaveAsync();

        Assert.AreEqual(1, result.Errors.Count);
    }

    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod, TestCategory("Live")]
    public async Task TestAPIException()
    {
        LogExtension.Settings.Exception.APIs.AddRange(this.Helper.GetAPISetting());
        LogExtension.Settings.Exception.SaveLogOnFile = false;

        ProcessMessageDTO result = await new Exception("Test Exception")
            .SaveAsync(Level.Emergency, new StackFrame(0, true));

        this.Helper.AssertNoErrors(result);
    }

    /// <summary>
    /// Test logging the issue on the system event.
    /// </summary>
    [TestMethod, TestCategory("Live")]
    public async Task TesLogOnSystemEvent()
    {
        LogExtension.Settings.Exception.SaveIssueOnEventSystem = true;

        ProcessMessageDTO result = await new Exception("Test Exception on System Event")
            .SaveAsync(Level.Debug);

        this.Helper.AssertNoErrors(result);
    }

    /// <summary>
    /// Test that a level left out of the lists is saved nowhere.
    /// </summary>
    [TestMethod]
    public async Task TesIgnoreLevelToLog()
    {
        LogExtension.Settings.Exception.LevelsToSaveOnFile.Add(Level.Warning);
        LogExtension.Settings.Exception.LevelsToLogOnSystemEvent.Add(Level.Warning);
        LogExtension.Settings.Exception.SaveIssueOnEventSystem = true;

        ProcessMessageDTO result = await new Exception("Test Exception on System Event")
            .SaveAsync(Level.Debug);

        this.Helper.AssertNoErrors(result);
        Assert.IsFalse(Directory.Exists(this.Helper.ExceptionsFolder));
    }
}
