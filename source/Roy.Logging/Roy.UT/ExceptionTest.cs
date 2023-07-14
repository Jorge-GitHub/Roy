using Roy.Domain.Contants;
using Roy.Domain.Settings;
using Roy.Domain.Settings.Web.EmailAspect;
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
        SettingExtension.Settings.Exception.FileName = "test.txt";
        string fileLocation = new UTHelper().GetfullPathToFile(
            SettingExtension.Settings.Exception.DefaultFolderName,
            SettingExtension.Settings.Exception.FileName);
        new Exception("Test file Name Exception").SaveAsync();
        SettingExtension.Settings.Exception.FileName = string.Empty;
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
        SettingExtension.Settings.Exception.Emails.Add(
            new UTHelper().GetEmailSetting());
        new Exception("Test Exception").SaveAsync(Level.Emergency, new StackFrame(1, true));
    }


    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod]
    public void TestEmailExceptionFrenchVersion()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr");
        SettingExtension.Settings.Exception.Emails.Add(
            new UTHelper().GetEmailSetting());
        new Exception("Test Exception").SaveAsync(Level.Emergency, new StackFrame(1, true));
    }

    /// <summary>
    /// Test that a particular level error will not be send to the user.
    /// </summary>
    [TestMethod]
    public void TestEmailExceptionNotSendingEmailToLevel()
    {
        EmailSetting setting = new UTHelper().GetEmailSetting();
        setting.LevelsToReport.Add(Level.Warning);
        SettingExtension.Settings.Exception.Emails.Add(setting);
        SettingExtension.Settings.Exception.SaveLogOnFile = false;
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
        SettingExtension.Settings.Exception.APIs.AddRange(
            new UTHelper().GetAPISetting());
        SettingExtension.Settings.Exception.SaveLogOnFile = false;
        new Exception("Test Exception").SaveAsync(Level.Emergency, new StackFrame(1, true));
    }
}