using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.StringExtensions;
using MimeKit;
using Roy.Domain.Contants;
using Roy.Domain.Settings.Web.EmailAspect;
using Roy.Logging.Resources;

namespace Roy.Logging.Aspect.Email;

internal static class EmailSettingExtensions
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
    /// <returns>
    /// MimeMessage.
    /// </returns>
    public static MimeMessage ToMimeMessage(this EmailSetting setting, ReceiverSetting receiver)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress(
            setting.DisplayNameFrom, setting.From));
        message.Subject = receiver.Subject.ToDefaultValueIfEmpty(
            setting.DefaultEmailSubject);
        message.To.AddEmails(receiver.To);
        message.Bcc.AddEmails(receiver.BCC);
        message.Cc.AddEmails(receiver.CC);
        message.Body = setting.ToMessageBody(receiver);
        
        return message;
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
    /// <returns>
    /// Message body.
    /// </returns>
    public static MimeEntity ToMessageBody(this EmailSetting setting, 
        ReceiverSetting receiver)
    {
        BodyBuilder builder = new BodyBuilder();
        bool isTextBody = receiver.IsTextBody.HasValue ? 
            receiver.IsTextBody.Value : setting.DefaultIsTextBody;
        // SET BODY HERE.
        string body = receiver.Body.ToDefaultValueIfEmpty(
            setting.DefaultEmailBody);
        if (!isTextBody)
        {
            builder.HtmlBody = body;
        }
        else
        {
            builder.TextBody = body;
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
    /// Set the default values.
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
    public static void SetDefaultValues(this EmailSetting setting, 
        Level level, bool isAnException)
    {
        if (setting.DefaultEmailSubject.IsNullOrEmpty())
        {
            string logging = isAnException ? StringValues.ExceptionLabel 
                : StringValues.LoggingLabel;
            setting.DefaultEmailSubject = $"Roy {logging} - Issue Level: {level.ToString()}";
        }

        if (setting.DefaultEmailBody.IsNullOrEmpty())
        {
            setting.DefaultEmailBody = RoyValues.ResourceManager.GetString(
                StringValues.DefaultHTMLBodyId);
        }
    }
}