using Avalon.Base.Extension.Types;
using MailKit.Net.Smtp;
using MimeKit;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Communication;
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
    /// <param name="process">
    /// Message returned by the logging service.
    /// </param>
    public void Send(EmailSetting setting, MessageDetail message, 
        InformationSetting settings, ProcessMessage process)
    {
        setting.SetDefaultValues(message.Level,
            message.IsExceptionType(), message.Id);
        foreach (ReceiverSetting receiver in setting.Receivers)
        {
            try
            {
                this.Send(setting, receiver, message, settings);
            }
            catch (Exception ex)
            {
                process.Errors.Add(ex);
            }
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
        MessageDetail bodyDetail, InformationSetting settings)
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
        catch
        {
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}