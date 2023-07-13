using Avalon.Base.Extension.Types;
using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using Roy.Domain.Settings;
using Roy.Logging.Helpers;
using System.Diagnostics;

namespace Roy.Logging;

/// <summary>
/// Log extensions.
/// </summary>
public static class LogExtension
{
    /// <summary>
    /// Log settings.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    public static void LogAsync<TValue>(this TValue value)
    {
        value.LogAsync(string.Empty, string.Empty, SettingExtension.Settings, null);
    }

    /// <summary>
    /// Log settings.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    public static void LogAsync<TValue>(this TValue value, StackFrame frame)
    {
        value.LogAsync(string.Empty, string.Empty, SettingExtension.Settings, frame);
    }

    /// <summary>
    /// Log settings.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        RoySetting settings)
    {
        value.LogAsync(string.Empty, string.Empty, settings, null);
    }

    /// <summary>
    /// Log settings.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    /// <param name="message">
    /// Log's message.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        string message)
    {
        value.LogAsync(message, string.Empty, SettingExtension.Settings, null);
    }

    /// <summary>
    /// Log settings.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    /// <param name="identity">
    /// Log's Id.
    /// </param>
    /// <param name="message">
    /// Custom message.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        string message, string identity)
    {
        value.LogAsync(message, identity, SettingExtension.Settings, null);
    }

    /// <summary>
    /// Log settings.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    /// <param name="identity">
    /// Log's Id.
    /// </param>
    /// <param name="message">
    /// Custom message.
    /// </param>
    /// <param name="setting">
    /// Log's settings.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        string message, string identity,  RoySetting setting, StackFrame frame)
    {
        try
        {
            setting = setting ?? SettingExtension.Settings;
            if (value != null && setting != null && !setting.Log.Disable)
            {                
                identity = identity.IsNotNullOrEmpty() ? identity
                    : Guid.NewGuid().ToString("N");
                
                LogDetail detail = new LogDetail(value, identity, 
                    message, frame, setting.Log.LoadSystemInformation);

                new RegisterService().SaveAsync(
                    detail, setting.Log, Level.Log);
            }
        }
        catch { }
    }
}