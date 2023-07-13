using Avalon.Base.Extension.Collections;
using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using Roy.Domain.Settings.Web.EmailAspect;
using Roy.Logging.Aspect.Email;

namespace Roy.Logging.Helpers;

/// <summary>
/// Email service.
/// </summary>
public class EmailService
{
    private EmailUtility Utility { get; set; } = new EmailUtility();
    /// <summary>
    /// Send exception/log.
    /// </summary>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="level">
    /// Exception/log's Level.
    /// </param>
    /// <param name="Emails">
    /// Emails to process.
    /// </param>
    public async void SendAsync(MessageDetail bodyDetail, Level level,
        List<EmailSetting> settings)
    {
        if (settings.HasElements())
        {
            foreach (EmailSetting setting in settings)
            {
                if ((!setting.LevelsToReport.HasElements() || 
                    setting.LevelsToReport.Any(item => item.Equals(level)))
                    && !setting.DisableEmailSending)
                {
                    this.Utility.Send(setting, bodyDetail, level);
                }
            }
        }
    }
}