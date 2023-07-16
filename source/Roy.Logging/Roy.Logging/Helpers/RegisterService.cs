using Avalon.Base.Extension.Collections;
using Roy.Domain.Attributes;
using Roy.Domain.Settings.Attributes;

namespace Roy.Logging.Helpers;

/// <summary>
/// Register service.
/// </summary>
internal class RegisterService
{
    /// <summary>
    /// Save message.
    /// </summary>
    /// <param name="message">
    /// Message to save.
    /// </param>
    /// <param name="setting">
    /// Settings.
    /// </param>
    public async void SaveAsync(MessageDetail message, IssueSetting setting)
    {
        try
        {
            if (setting.SaveLogOnFile)
            {
                new FileService().SaveAsync(message, setting);
            }
        }
        catch { }

        try
        {
            if (setting.Emails.HasElements())
            {
                new EmailService().SendAsync(message, setting.Emails);
            }
        }
        catch { }

        try
        {
            if (setting.APIs.HasElements())
            {
                new APIService().PostAsync(message, setting.APIs);
            }
        }
        catch { }


        try
        {
            if (setting.SaveIssueOnEventSystem)
            {
                new SystemEventLogService().LogAsync(message);
            }
        }
        catch { }
    }
}