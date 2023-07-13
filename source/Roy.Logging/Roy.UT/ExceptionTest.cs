using Roy.Domain.Settings.Web.EmailAspect;
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
        LogExtension.Settings.Exception.FileName = "test.txt";
        string fileLocation = new UTHelper().GetfullPathToFile(
            LogExtension.Settings.Exception.DefaultFolderName,
            LogExtension.Settings.Exception.FileName);
        new Exception("Test file Name Exception").SaveAsync();
        LogExtension.Settings.Exception.FileName = string.Empty;
        Assert.IsTrue(File.Exists(fileLocation));
    }

    /// <summary>
    /// Default test execution.
    /// </summary>
    [TestMethod]
    public void TestEmailException()
    {
        LogExtension.Settings.Exception.Emails.Add(this.GetEmailSetting());
        new Exception("Test Exception").SaveAsync(new StackFrame(1, true));
    }

    /// <summary>
    /// Get the email settings.
    /// </summary>
    /// <returns>
    /// Email settings.
    /// </returns>
    private EmailSetting GetEmailSetting()
    {
        EmailSetting settings = new EmailSetting();
        settings.Server.Host = "smtp.ethereal.email";
        settings.From = "roy@yahoo.com";
        settings.DefaultIsTextBody = false;
        settings.UserAccount = "robbie.hermiston@ethereal.email";
        settings.UserPassword = "76kqGgRwUfDF2gCQ66";
        ReceiverSetting receiver = new ReceiverSetting();
        receiver.To = "royorbinson@gmail.com";
        settings.Receivers.Add(receiver);

        return settings;
    }
}
