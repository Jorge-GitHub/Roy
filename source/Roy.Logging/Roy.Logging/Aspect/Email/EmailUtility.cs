using Avalon.Base.Extensions.Types;
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
    public async Task SendAsync(EmailSetting setting, MessageDetail message, 
        InformationSetting settings, InternalProcessMessage process)
    {
        setting.SetDefaultValues(message.Level,
            message.IsExceptionType(), message.Id);
        foreach (ReceiverSetting receiver in setting.Receivers)
        {
            try
            {
                await this.SendAsync(setting, receiver, message, settings)
                    .ConfigureAwait(false);
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
    private async Task SendAsync(EmailSetting setting, ReceiverSetting receiver, 
        MessageDetail bodyDetail, InformationSetting settings)
    {
        MimeMessage message = setting.ToMimeMessage(receiver, bodyDetail, settings);
        using (SmtpClient client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(setting.Server.Host, setting.Server.Port,
                    setting.EnableSSL).ConfigureAwait(false);
                if (setting.UserAccount.IsNotNullOrEmpty())
                {
                    await client.AuthenticateAsync(setting.UserAccount,
                        setting.UserPassword).ConfigureAwait(false);
                }
                if (setting.Server.TimeOut > 0)
                {
                    client.Timeout = setting.Server.TimeOut;
                }
                await client.SendAsync(message).ConfigureAwait(false);
            }
            finally
            {
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
