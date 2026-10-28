using Roy.Logging;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.DTO.Communication;
using Roy.Logging.Domain.Settings;
using Roy.UT.Entities;
using System.Diagnostics;
using System.Text.Json;

namespace Roy.UT;

/// <summary>
/// Log tests.
/// </summary>
[TestClass]
public class LogTest
{
    /// <summary>
    /// This test's helper and folder.
    /// </summary>
    private UTHelper Helper { get; set; } = new UTHelper();

    /// <summary>
    /// Fresh settings and an empty folder for every test.
    /// </summary>
    [TestInitialize]
    public void Initialize()
    {
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
    /// Default test log execution.
    /// </summary>
    [TestMethod]
    public async Task TestLog()
    {
        Artist utObject = this.Helper.GetDefaultSampleObject();

        ProcessMessageDTO result = await utObject.LogAsync();

        this.Helper.AssertNoErrors(result);
        Assert.IsTrue(result.Succeed);
        string file = this.Helper.GetSingleFile(this.Helper.LogsFolder);
        StringAssert.StartsWith(Path.GetFileName(file), "Log.");
        JsonElement json = this.Helper.ReadJson(file);
        Assert.AreEqual("Roy", json.GetProperty("LogValue").GetProperty("Name").GetString());
        Assert.AreEqual((int)Level.Log, json.GetProperty("Level").GetInt32());
    }

    /// <summary>
    /// Test that disabling the logs saves nothing and fails nothing.
    /// </summary>
    [TestMethod]
    public async Task TestLogDisabled()
    {
        LogExtension.Settings.Log.Disable = true;

        ProcessMessageDTO result = await this.Helper.GetDefaultSampleObject().LogAsync();

        Assert.IsTrue(result.Succeed);
        Assert.IsFalse(Directory.Exists(this.Helper.LogsFolder));
    }

    /// <summary>
    /// Test logging a primitive.
    /// </summary>
    [TestMethod]
    public async Task TestPrimitiveLog()
    {
        ProcessMessageDTO result = await "Anything you want, you got it".LogAsync();

        this.Helper.AssertNoErrors(result);
        string file = this.Helper.GetSingleFile(this.Helper.LogsFolder);
        Assert.AreEqual("Anything you want, you got it",
            this.Helper.ReadJson(file).GetProperty("LogValue").GetString());
    }

    /// <summary>
    /// Test that setting the file name will work.
    /// </summary>
    [TestMethod]
    public async Task TestFileNameLog()
    {
        Artist utObject = this.Helper.GetDefaultSampleObject();
        LogExtension.Settings.Log.FileName = "testLog.txt";
        string fileLocation = this.Helper.GetfullPathToFile(
            this.Helper.LogsFolder, LogExtension.Settings.Log.FileName);

        await utObject.LogAsync();

        Assert.IsTrue(File.Exists(fileLocation));
        Assert.AreEqual("Roy", this.Helper.ReadJson(fileLocation)
            .GetProperty("LogValue").GetProperty("Name").GetString());
    }

    /// <summary>
    /// Test loading the method information. Synchronous on purpose: the frame
    /// must belong to this method, not to an async state machine.
    /// </summary>
    [TestMethod]
    public void TestLogSystemInformation()
    {
        LogExtension.Settings.Log.LoadInformationSettings.LogMethodInformation = true;
        Artist utObject = this.Helper.GetDefaultSampleObject();

        ProcessMessageDTO result = utObject.LogAsync(new StackFrame(0, true))
            .GetAwaiter().GetResult();

        this.Helper.AssertNoErrors(result);
        JsonElement frame = this.Helper.ReadJson(
            this.Helper.GetSingleFile(this.Helper.LogsFolder)).GetProperty("StackFrame");
        Assert.AreEqual("TestLogSystemInformation", frame.GetProperty("CallerMethodName").GetString());
        StringAssert.EndsWith(frame.GetProperty("CallerFileName").GetString(), "LogTest.cs");
        Assert.IsTrue(frame.GetProperty("CallerLineNumber").GetInt32() > 0);
    }

    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod, TestCategory("Live")]
    public async Task TestEmailLog()
    {
        LogExtension.Settings.Log.Emails.Add(this.Helper.GetEmailSetting());
        Artist utObject = this.Helper.GetDefaultSampleObject();

        ProcessMessageDTO result = await utObject.LogAsync(new StackFrame(0, true));

        this.Helper.AssertNoErrors(result);
    }
}
