using MimeKit;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using Roy.Logging.Extensions;
using Roy.UT.Entities;

namespace Roy.UT;

/// <summary>
/// Email tests that need no server.
/// </summary>
[TestClass]
public class EmailTest
{
    /// <summary>
    /// Test that each email's subject carries its own issue's Id, in the order
    /// EmailUtility runs: SetDefaultValues, then ToMimeMessage, per message.
    /// </summary>
    [TestMethod]
    public void TestEachSubjectHasItsOwnId()
    {
        EmailSetting setting = new UTHelper().GetEmailSetting();
        ReceiverSetting receiver = setting.Receivers[0];
        InformationSetting information = new InformationSetting();
        ExceptionDetail first = new ExceptionDetail(new Exception("one"), Level.Error,
            "id-one", string.Empty, null!, information, null!, null!);
        ExceptionDetail second = new ExceptionDetail(new Exception("two"), Level.Error,
            "id-two", string.Empty, null!, information, null!, null!);

        setting.SetDefaultValues(first.Level, true, first.Id);
        MimeMessage firstMessage = setting.ToMimeMessage(receiver, first, information);
        setting.SetDefaultValues(second.Level, true, second.Id);
        MimeMessage secondMessage = setting.ToMimeMessage(receiver, second, information);

        StringAssert.Contains(firstMessage.Subject, "id-one");
        StringAssert.Contains(secondMessage.Subject, "id-two");
        Assert.IsTrue(string.IsNullOrEmpty(setting.DefaultEmailSubject),
            "The setting must not keep a subject with an Id baked in.");
    }

    /// <summary>
    /// Test that a subject set on the setting still gets the Id filled in.
    /// </summary>
    [TestMethod]
    public void TestCustomSubjectGetsTheId()
    {
        EmailSetting setting = new UTHelper().GetEmailSetting();
        setting.DefaultEmailSubject = "Roy says *|IssueId|* is *|Level|*";
        InformationSetting information = new InformationSetting();
        ExceptionDetail detail = new ExceptionDetail(new Exception("one"), Level.Error,
            "id-one", string.Empty, null!, information, null!, null!);

        setting.SetDefaultValues(detail.Level, true, detail.Id);
        MimeMessage message = setting.ToMimeMessage(setting.Receivers[0], detail, information);

        Assert.AreEqual("Roy says id-one is Error", message.Subject);
    }
}
