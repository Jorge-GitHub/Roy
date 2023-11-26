using Avalon.Base.Extension.Types;
using MailKit.Net.Smtp;
using MimeKit;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using Roy.Logging.Extensions;

namespace Roy.Logging.Aspect.Email;

/// <summary>
/// Email service.
/// </summary>
internal class EmailUtility
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="setting">
    /// Email settings.
    /// </param>
    /// <param name="message">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="settings">
    /// Log settings.
    /// </param>
    public void Send(EmailSetting setting, MessageDetail message, LogSetting settings)
    {
        setting.SetDefaultValues(message.Level,
            message.IsExceptionType(), message.Id);
        foreach (ReceiverSetting receiver in setting.Receivers)
        {
            this.Send(setting, receiver, message, settings);
        }
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="setting">
    /// Email settings object.
    /// </param>
    /// <param name="receiver">
    /// Receiver to get the exception/log.
    /// </param>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="settings">
    /// Log settings.
    /// </param>
    private void Send(EmailSetting setting, ReceiverSetting receiver, 
        MessageDetail bodyDetail, LogSetting settings)
    {
        MimeMessage message = setting.ToMimeMessage(receiver, bodyDetail, settings);
        SmtpClient client = new SmtpClient();
        try
        {
            client.Connect(setting.Server.Host, setting.Server.Port, setting.EnableSSL);
            if (setting.UserAccount.IsNotNullOrEmpty())
            {
                client.Authenticate(setting.UserAccount, setting.UserPassword);
            }
            if (setting.Server.TimeOut > 0)
            {
                client.Timeout = setting.Server.TimeOut;
            }
            client.Send(message);
        }
        catch { } // We let the system keep sending emails to the other recipients.
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}