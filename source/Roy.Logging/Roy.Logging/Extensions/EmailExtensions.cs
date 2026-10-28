using Avalon.Base.Extensions.Collections;
using Avalon.Base.Extensions.Types;
using Avalon.Base.Extensions.Types.StringExtensions;
using MimeKit;
using Roy.Logging.Aspect.Email;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using Roy.Logging.Resources;
using Roy.Logging.Resources.Languages.EmailTemplate;
using System.Text;

namespace Roy.Logging.Extensions;

internal static class EmailExtensions
{
    /// <summary>
    /// Convert an EmailSetting into a MimeMessage.
    /// </summary>
    /// <param name="setting">
    /// EmailSetting to convert from.
    /// </param>
    /// <param name="receiver">
    /// Receiver settings.
    /// </param>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="settings">
    /// Log settings.
    /// </param>
    /// <returns>
    /// MimeMessage.
    /// </returns>
    public static MimeMessage ToMimeMessage(this EmailSetting setting,
        ReceiverSetting receiver, MessageDetail bodyDetail, InformationSetting settings)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress(
            setting.DisplayNameFrom, setting.From));
        message.Subject = setting.GetSubject(receiver, bodyDetail);
        message.To.AddEmails(receiver.To);
        message.Bcc.AddEmails(receiver.BCC);
        message.Cc.AddEmails(receiver.CC);
        message.Body = setting.ToMessageBody(receiver, bodyDetail, settings);

        return message;
    }

    /// <summary>
    /// Get the subject for this message: the receiver's, else the setting's default,
    /// else the resource's, with the issue's Id and level filled in. The setting is
    /// left as it is, so the next message gets its own Id.
    /// </summary>
    /// <param name="setting">
    /// Email settings.
    /// </param>
    /// <param name="receiver">
    /// Receiver settings.
    /// </param>
    /// <param name="bodyDetail">
    /// Message the email is about.
    /// </param>
    /// <returns>
    /// Subject.
    /// </returns>
    public static string GetSubject(this EmailSetting setting,
        ReceiverSetting receiver, MessageDetail bodyDetail)
    {
        string template = receiver.Subject.ToDefaultValueIfEmpty(
            setting.DefaultEmailSubject);
        if (template.IsNullOrEmpty())
        {
            string subjectHolderId = bodyDetail.IsExceptionType() ?
                ResourceKey.ExceptionSubject : ResourceKey.RoyLoginSubject;
            template = EmailLabels.ResourceManager
                .GetString(subjectHolderId, setting.Culture) ?? string.Empty;
        }
        StringBuilder subject = new StringBuilder(template);
        subject.Replace(EmailLabel.IssueIdTag, bodyDetail.Id);
        subject.Replace(EmailLabel.LevelTag,
            bodyDetail.Level.ToCurrentCultureString(setting.Culture));

        return subject.ToString();
    }

    /// <summary>
    /// To message body.
    /// </summary>
    /// <param name="message">
    /// EmailMessage to convert from.
    /// </param>
    /// <param name="receiver">
    /// Receiver settings.
    /// </param>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="settings">
    /// Log settings.
    /// </param>
    /// <returns>
    /// Message body.
    /// </returns>
    public static MimeEntity ToMessageBody(this EmailSetting setting,
        ReceiverSetting receiver, MessageDetail bodyDetail,
        InformationSetting settings)
    {
        BodyBuilder builder = new BodyBuilder();
        bool isTextBody = receiver.IsTextBody.HasValue ?
            receiver.IsTextBody.Value : setting.DefaultIsTextBody;
        string body = receiver.Body.ToDefaultValueIfEmpty(
                setting.DefaultEmailBody);
        body = new Decorator().GenerateBody(
            body, bodyDetail, setting.Culture, settings);
        if (isTextBody)
        {
            builder.TextBody = body;
        }
        else
        {
            builder.HtmlBody = body;
        }

        return builder.ToMessageBody();
    }

    /// <summary>
    /// Add a list of emails to the address list.
    /// </summary>
    /// <param name="addressList">
    /// AddressList to add the emails.
    /// </param>
    /// <param name="emails">
    /// Emails to add.
    /// </param>
    public static void AddEmails(this InternetAddressList addressList,
        string emails)
    {
        if (emails.IsNotNullOrEmpty())
        {
            List<MailboxAddress> addresses = new List<MailboxAddress>();
            string[] listOfEmails = emails.Split(';');
            if (listOfEmails.HasElements())
            {
                foreach (string emailAddress in listOfEmails)
                {
                    addresses.Add(MailboxAddress.Parse(emailAddress));
                }
                addressList.AddRange(addresses);
            }
        }
    }

    /// <summary>
    /// Set the default values: the culture, and the body template. The subject is
    /// built per message by GetSubject.
    /// </summary>
    /// <param name="setting">
    /// Email settings.
    /// </param>
    /// <param name="level">
    /// Issue level.
    /// </param>
    /// <param name="isAnException">
    /// Flag that determinate whether the issue is an exception or a log.
    /// </param>
    /// <param name="issueId">
    /// Issue's ID.
    /// </param>
    public static void SetDefaultValues(this EmailSetting setting,
        Level level, bool isAnException, string issueId)
    {
        setting.Culture = setting.Language.ToCultureInfo();

        if (setting.DefaultEmailBody.IsNullOrEmpty())
        {
            string htmlBodyId = isAnException ?
                ResourceKey.ExceptionHTMLBodyId : ResourceKey.LogHTMLBodyId;
            setting.DefaultEmailBody = RoyValues.ResourceManager
                .GetString(htmlBodyId);
        }
    }
}
