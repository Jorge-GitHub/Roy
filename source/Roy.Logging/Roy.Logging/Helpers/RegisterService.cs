﻿using Avalon.Base.Extension.Collections;
using Roy.Domain.Attributes;
using Roy.Domain.Contants;
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
    /// <param name="level">
    /// Register (exception/log) level.
    /// </param>
    public async void SaveAsync(MessageDetail message,
        Setting setting, Level level)
    {
        try
        {
            if (setting.SaveLogOnFile)
            {
                new FileService().SaveAsync(message, message.Id,
                    setting?.FolderLocation, setting?.FileName,
                    level, setting?.DefaultFolderName,
                    setting.Append);
            }
        } catch { }

        try
        {
            if (setting.Emails.HasElements())
            {
                new EmailService().SendAsync(message,
                level, setting?.Emails);
            }
        } catch { }
    }
}