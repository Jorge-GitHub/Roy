using Avalon.Base.Extension.Types;
using MailKit.Net.Smtp;
using MimeKit;
using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using Roy.Domain.Settings.Web.EmailAspect;

namespace Roy.Logging.Aspect.Email;

internal class EmailUtility
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="setting">
    /// Email settings object.
    /// </param>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="level">
    /// Exception/log's Level.
    /// </param>
    public void Send(EmailSetting setting, MessageDetail bodyDetail, Level level)
    {
        setting.SetDefaultValues(level, bodyDetail is ExceptionDetail);
        foreach (ReceiverSetting receiver in setting.Receivers)
        {
            this.Send(setting, receiver, bodyDetail);
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
    private void Send(EmailSetting setting, ReceiverSetting receiver, 
        MessageDetail bodyDetail)
    {
        MimeMessage message = setting.ToMimeMessage(receiver);
        SmtpClient client = new SmtpClient();
        try
        {
            client.Connect(setting.Server.Host, setting.Server.Port);
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
        catch { throw; }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}