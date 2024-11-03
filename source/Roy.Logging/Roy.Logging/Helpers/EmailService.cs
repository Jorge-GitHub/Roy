using Avalon.Base.Extension.Collections;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using Roy.Logging.Aspect.Email;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Domain.Communication;

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
    /// <param name="process">
    /// Message returned by the logging service.
    /// </param>
    public async void SendAsync(MessageDetail bodyDetail, 
        List<EmailSetting> emails, InformationSetting settings,
        InternalProcessMessage process)
    {
        foreach (EmailSetting setting in emails)
        {
            try
            {
                if ((!setting.LevelsToReport.HasElements() ||
                    setting.LevelsToReport.Any(item => item.Equals(bodyDetail.Level)))
                    && !setting.DisableEmailSending)
                {
                    this.Utility.Send(setting, bodyDetail, settings, process);
                }
            }
            catch (Exception ex)
            {
                process.Errors.Add(ex);
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