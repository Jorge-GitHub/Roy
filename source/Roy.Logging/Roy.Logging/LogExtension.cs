using Avalon.Base.Extension.Types;
using Roy.Domain;
using Roy.Domain.Contants;
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
    public static LogSetting Settings { get; set; } = new LogSetting();

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
        LogSetting settings)
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
        string message, string identity,  LogSetting setting, StackFrame frame)
    {
        try
        {
            if (value != null)
            {
                setting = setting ?? LogExtension.Settings;
                identity = identity.IsNotNullOrEmpty() ? identity
                    : Guid.NewGuid().ToString("N");
               
                bool appendLog = setting != null ? setting.AppendException : false;
                bool logSystemInformation = setting != null ?
                    setting.LogSystemInformation : false;

                LogDetail detail = new LogDetail(value, Level.Log,
                    identity, message, frame, logSystemInformation);

                new FileService().SaveAsync(detail,
                    identity, setting?.LogFolderLocation,
                    setting?.LogFileName,
                    Level.Log, setting?.LogDefaultFolderName,
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
    public static LogSetting CopySettings()
    {
        return LogExtension.Settings.Copy<LogSetting>();
    }
}