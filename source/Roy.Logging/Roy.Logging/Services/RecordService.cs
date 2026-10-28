using Avalon.Base.Extensions.Collections;
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
    public async Task<InternalProcessMessage> SaveAsync(MessageDetail message,
        IssueSetting setting)
    {
        InternalProcessMessage process = new InternalProcessMessage();
        await this.SaveInternalAsync(message, setting, process).ConfigureAwait(false);
        await this.SaveExternalAsync(message, setting, process).ConfigureAwait(false);

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
    private async Task SaveInternalAsync(MessageDetail message, IssueSetting setting, 
        InternalProcessMessage process)
    {
        try
        {
            if (setting.SaveLogOnFile)
            {
                await new FileService().SaveAsync(message, setting).ConfigureAwait(false);
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
                new SystemEventLogService().Log(message, setting);
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
    private async Task SaveExternalAsync(MessageDetail message, IssueSetting setting, 
        InternalProcessMessage process)
    {
        try
        {
            if (setting.Emails.HasElements())
            {
                await new EmailService().SendAsync(message, setting.Emails,
                    setting.LoadInformationSettings, process).ConfigureAwait(false);
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
                await new APIService().PostAsync(message, setting.APIs, process)
                    .ConfigureAwait(false);
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
                await new DatabaseService().SaveAsync(message,
                    setting.Databases, process).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            process.Errors.Add(ex);
        }
    }
}
