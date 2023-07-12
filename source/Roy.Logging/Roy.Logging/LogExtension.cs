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
    public static RoySetting Settings { get; set; } = new RoySetting();

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
        value.LogAsync(string.Empty, string.Empty, LogExtension.Settings, null);
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
        value.LogAsync(string.Empty, string.Empty, LogExtension.Settings, frame);
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
        value.LogAsync(message, string.Empty, LogExtension.Settings, null);
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
        value.LogAsync(message, identity, LogExtension.Settings, null);
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
            setting = setting ?? LogExtension.Settings;
            if (value != null && setting != null && setting.Log.Enable)
            {                
                identity = identity.IsNotNullOrEmpty() ? identity
                    : Guid.NewGuid().ToString("N");
               
                bool appendLog = setting != null ? setting.Log.Append : false;
                bool logSystemInformation = setting != null ?
                    setting.Log.LoadSystemInformation : false;

                LogDetail detail = new LogDetail(value, Level.Log,
                    identity, message, frame, logSystemInformation);

                new FileService().SaveAsync(detail,
                    identity, setting?.Log.FolderLocation,
                    setting?.Log.FileName,
                    Level.Log, setting?.Log.DefaultFolderName,
                    appendLog);
            }
        }
        catch { }
    }

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