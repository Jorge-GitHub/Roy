using Avalon.Base.Extension.Collections;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using Roy.Logging.Aspect.Email;
using Roy.Logging.Domain.Settings.Attributes;

namespace Roy.Logging.Helpers;

/// <summary>
/// Email service.
/// </summary>
public class EmailService
{
    private EmailUtility Utility { get; set; }
    /// <summary>
    /// Send exception/log.
    /// </summary>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="emails">
    /// Emails to process.
    /// </param>
    /// <param name="settings">
    /// Log settings.
    /// </param>
    public async void SendAsync(MessageDetail bodyDetail, 
        List<EmailSetting> emails, InformationSetting settings)
    {
        foreach (EmailSetting setting in emails)
        {
            if ((!setting.LevelsToReport.HasElements() ||
                setting.LevelsToReport.Any(item => item.Equals(bodyDetail.Level)))
                && !setting.DisableEmailSending)
            {
                this.Utility.Send(setting, bodyDetail, settings);
            }
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public EmailService()
    {
        this.Utility = new EmailUtility();
    }
}