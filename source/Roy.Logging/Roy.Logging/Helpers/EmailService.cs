using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using Roy.Domain.Settings.Web.EmailAspect;

namespace Roy.Logging.Helpers;

/// <summary>
/// Email helper.
/// </summary>
public class EmailService
{
    /// <summary>
    /// Send exception/log.
    /// </summary>
    /// <param name="value">
    /// Value to send.
    /// </param>
    /// <param name="level">
    /// Exception/log's Level.
    /// </param>
    /// <param name="Emails">
    /// Emails to process.
    /// </param>
    public async void SendAsync(MessageDetail value, Level level, List<EmailSetting> Emails)
    {
        var detail = (value is LogDetail) ? value as MessageDetail : value as ExceptionDetail;
    }
}