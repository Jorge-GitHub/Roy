using Roy.Logging;
using Roy.UT.Entities;
using System.Diagnostics;

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
        LogExtension.Settings.ExceptionFileName = "test.txt";
        string fileLocation = new UTHelper().GetfullPathToFile(
            LogExtension.Settings.ExceptionDefaultFolderName,
            LogExtension.Settings.ExceptionFileName);
        new Exception("Test file Name Exception").SaveAsync();
        LogExtension.Settings.ExceptionFileName = string.Empty;
        Assert.IsTrue(File.Exists(fileLocation));
    }
}
