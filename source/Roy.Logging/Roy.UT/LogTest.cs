using Roy.Domain.Settings;
using Roy.Logging;
using Roy.UT.Entities;
using System.Diagnostics;

namespace Roy.UT;

/// <summary>
/// Exception tests.
/// </summary>
[TestClass]
public class LogTest
{
    /// <summary>
    /// Default test log execution.
    /// </summary>
    [TestMethod]
    public void TestLog()
    {
        Artist utObject = new UTHelper().GetDefaultSampleObject();
        utObject.LogAsync();
    }

    /// <summary>
    /// Test that setting the file name will work.
    /// </summary>
    [TestMethod]
    public void TestFileNameLog()
    {
        UTHelper helper = new UTHelper();
        Artist utObject = helper.GetDefaultSampleObject();
        SettingExtension.Settings.Log.FileName = "testLog.txt";
        string fileLocation = helper.GetfullPathToFile(
            SettingExtension.Settings.Log.DefaultFolderName,
            SettingExtension.Settings.Log.FileName);
        utObject.LogAsync();
        SettingExtension.Settings.Log.FileName = string.Empty;
        Assert.IsTrue(File.Exists(fileLocation));
    }

    /// <summary>
    /// Test loading the system information.
    /// </summary>
    [TestMethod]
    public void TestLogSystemInformation()
    {
        SettingExtension.Settings.Log.LoadSystemInformation = true;
        Artist utObject = new UTHelper().GetDefaultSampleObject();
        utObject.LogAsync(new StackFrame(0, true));
        SettingExtension.Settings.Log.LoadSystemInformation = false;
    }

    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod]
    public void TestEmailLog()
    {
        SettingExtension.Settings.Log.Emails.Add(
            new UTHelper().GetEmailSetting());
        Artist utObject = new UTHelper().GetDefaultSampleObject();
        utObject.LogAsync(new StackFrame(0, true));
    }
}