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
        LogExtension.Settings.LogFileName = "testLog.txt";
        string fileLocation = helper.GetfullPathToFile(
            LogExtension.Settings.LogDefaultFolderName,
            LogExtension.Settings.LogFileName);
        utObject.LogAsync();
        LogExtension.Settings.LogFileName = string.Empty;
        Assert.IsTrue(File.Exists(fileLocation));
    }

    /// <summary>
    /// Test loading the system information.
    /// </summary>
    [TestMethod]
    public void TestLogSystemInformation()
    {
        LogExtension.Settings.LogSystemInformation = true;
        Artist utObject = new UTHelper().GetDefaultSampleObject();
        utObject.LogAsync(new StackFrame(0, true));
        LogExtension.Settings.LogSystemInformation = false;
    }
}