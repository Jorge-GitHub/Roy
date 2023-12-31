﻿using Avalon.Base.Extension.Types;

namespace Roy.Logging.Domain.Settings;

/// <summary>
/// Log extensions settings.
/// </summary>
public static class LogExtension
{
    /// <summary>
    /// Log settings.
    /// </summary>
    public static RoySetting Settings { get; set; } = new RoySetting();

    /// <summary>
    /// Create a copy of the log settings.
    /// </summary>
    /// <returns>
    /// A copy of the log settings.
    /// </returns>
    public static RoySetting CopySettings()
    {
        return LogExtension.Settings.Copy<RoySetting>();
    }
}