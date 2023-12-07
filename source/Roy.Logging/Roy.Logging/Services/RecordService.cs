using Avalon.Base.Extension.Collections;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Communication;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Helpers;

namespace Roy.Logging.Services;

/// <summary>
/// Record register service.
/// </summary>
internal class RecordService
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public ProcessMessage Save(MessageDetail message, IssueSetting setting)
    {
        ProcessMessage process = new ProcessMessage();
        this.SaveInternal(message, setting, process);
        this.SaveExternal(message, setting, process);

        return process;
    }

    /// <summary>
    /// Save message internally (files/event system).
    /// </summary>
    /// <param name="message">
    /// Message to save.
    /// </param>
    /// <param name="setting">
    /// Settings.
    /// </param>
    /// <param name="process">
    /// Message returned by the logging service.
    /// </param>
    private void SaveInternal(MessageDetail message, IssueSetting setting, 
        ProcessMessage process)
    {
        try
        {
            if (setting.SaveLogOnFile)
            {
                new FileService().Save(message, setting);
            }
        }
        catch (Exception ex)
        {
            process.Errors.Add(ex);
        }

        try
        {
            if (setting.SaveIssueOnEventSystem)
            {
                new SystemEventLogService().LogAsync(message, setting);
            }
        }
        catch (Exception ex)
        {
            process.Errors.Add(ex);
        }
    }

    /// <summary>
    /// Save message externally (email/api/database).
    /// </summary>
    /// <param name="message">
    /// Message to save.
    /// </param>
    /// <param name="setting">
    /// Settings.
    /// </param>
    /// <param name="process">
    /// Message returned by the logging service.
    /// </param>
    private void SaveExternal(MessageDetail message, IssueSetting setting, 
        ProcessMessage process)
    {
        try
        {
            if (setting.Emails.HasElements())
            {
                new EmailService().SendAsync(message, setting.Emails,
                    setting.LoadInformationSettings, process);
            }
        }
        catch (Exception ex)
        {
            process.Errors.Add(ex);
        }

        try
        {
            if (setting.APIs.HasElements())
            {
                new APIService().Post(message, setting.APIs, process);
            }
        }
        catch (Exception ex)
        {
            process.Errors.Add(ex);
        }

        try
        {
            if (setting.Databases.HasElements())
            {
                new DatabaseService().Save(message,
                    setting.Databases, process);
            }
        }
        catch (Exception ex)
        {
            process.Errors.Add(ex);
        }
    }
}