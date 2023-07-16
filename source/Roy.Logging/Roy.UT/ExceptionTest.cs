using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using Roy.Logging;
using Roy.UT.Entities;
using System.Diagnostics;
using System.Globalization;

namespace Roy.UT;

/// <summary>
/// Exception tests.
/// </summary>
[TestClass]
public class ExceptionTest
{
    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod]
    public void TestException()
    {
        new Exception("Test Exception").SaveAsync(new StackFrame(1, true));
    }

    /// <summary>
    /// Test that setting the file name will work.
    /// </summary>
    [TestMethod]
    public void TestFileNameException()
    {
        LogExtension.Settings.Exception.FileName = "test.txt";
        string fileLocation = new UTHelper().GetfullPathToFile(
            LogExtension.Settings.Exception.DefaultFolderName,
            LogExtension.Settings.Exception.FileName);
        new Exception("Test file Name Exception").SaveAsync();
        LogExtension.Settings.Exception.FileName = string.Empty;
        // We are running async so we need to wait a few seconds to be sure the file is there.
        Thread.Sleep(10000);
        Assert.IsTrue(File.Exists(fileLocation));
    }

    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod]
    public void TestEmailException()
    {
        LogExtension.Settings.Exception.Emails.Add(
            new UTHelper().GetEmailSetting());
        new Exception("Test Exception").SaveAsync(Level.Emergency, new StackFrame(1, true));
    }


    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod]
    public void TestEmailExceptionFrenchVersion()
    {
        LogExtension.Settings.Exception.Emails.Add(
            new UTHelper().GetEmailSetting());
        LogExtension.Settings.Exception.Emails[0].Language = Language.French;
        new Exception("Test Exception").SaveAsync(
            Level.Emergency, new StackFrame(1, true));
    }

    /// <summary>
    /// Test that a particular level error will not be send to the user.
    /// </summary>
    [TestMethod]
    public void TestEmailExceptionNotSendingEmailToLevel()
    {
        EmailSetting setting = new UTHelper().GetEmailSetting();
        setting.LevelsToReport.Add(Level.Warning);
        LogExtension.Settings.Exception.Emails.Add(setting);
        LogExtension.Settings.Exception.SaveLogOnFile = false;
        // This exception will not be send to the user because it is not an error
        // but a Warning. The default level log is Error.
        new Exception("Test Warning Not Send").SaveAsync(new StackFrame(1, true));
    }

    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod]
    public void TestAPIException()
    {
        LogExtension.Settings.Exception.APIs.AddRange(
            new UTHelper().GetAPISetting());
        LogExtension.Settings.Exception.SaveLogOnFile = false;
        new Exception("Test Exception").SaveAsync(Level.Emergency, new StackFrame(1, true));
    }

    /// <summary>
    /// Test logging the issue on the system event.
    /// </summary>
    [TestMethod]
    public void TesLogOnSystemEvent()
    {
        LogExtension.Settings.Exception.SaveIssueOnEventSystem = true;
        new Exception("Test Exception on System Event").SaveAsync(
            Level.Debug);
        Thread.Sleep(1000);
        LogExtension.Settings.Exception.SaveIssueOnEventSystem = false;
    }

    /// <summary>
    /// Test logging the issue on the system event.
    /// </summary>
    [TestMethod]
    public void TesIgnoreLevelToLog()
    {
        LogExtension.Settings.Exception.LevelsToSaveOnFile.Add(Level.Warning);
        LogExtension.Settings.Exception.LevelsToLogOnSystemEvent.Add(Level.Warning);
        LogExtension.Settings.Exception.SaveIssueOnEventSystem = true;
        new Exception("Test Exception on System Event")
            .SaveAsync(Level.Debug);
        Thread.Sleep(1000);
        LogExtension.Settings.Exception.SaveIssueOnEventSystem = false;
    }
}