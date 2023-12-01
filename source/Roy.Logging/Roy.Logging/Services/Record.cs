using Avalon.Base.Extension.Collections;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Helpers;

namespace Roy.Logging.Services;

/// <summary>
/// Register service.
/// </summary>
internal class Record
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
            if (setting.Databases.HasElements())
            {
                new DatabaseService().SaveAsync(message, setting.Databases);
            }
        } 
        catch { }

        try
        {
            if (setting.Emails.HasElements())
            {
                new EmailService().SendAsync(message, setting.Emails,
                    setting.LoadInformationSettings);
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
                new SystemEventLogService().LogAsync(message, setting);
            }
        }
        catch { }
    }
}