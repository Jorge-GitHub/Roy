﻿using Avalon.Base.Extension.Collections;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Helpers;

namespace Roy.Logging;

/// <summary>
/// Register service.
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
                new SystemEventLogService().LogAsync(message, setting);
            }
        }
        catch { }
    }
}