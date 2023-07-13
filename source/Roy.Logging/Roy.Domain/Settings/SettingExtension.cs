using Avalon.Base.Extension.Types;

namespace Roy.Domain.Settings;

public static class SettingExtension
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
        return SettingExtension.Settings.Copy<RoySetting>();
    }
}