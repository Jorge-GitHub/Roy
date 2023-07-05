using Avalon.Base.Extension.Types;
using Roy.Domain;
using Roy.Domain.Contants;
using Roy.Logging.Helpers;
using System.Diagnostics;
using System.Reflection;

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
        StackFrame frame = new StackFrame(1, true);
        string callerMemberName = frame.GetMethod()?.Name;
        string callerFilePath = frame.GetFileName();
        int callerLineNumber = frame.GetFileLineNumber();
        MethodBase test = frame.GetMethod();
        var parameters = test.GetParameters();
        // you can get the parameters by using: frame.GetMethod() MethodBase ParameterInfo
        value.LogAsync(string.Empty, LogExtension.Settings);
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
        value.LogAsync(string.Empty, settings);
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
    public static async void LogAsync<TValue>(this TValue value,
        string identity)
    {
        value.LogAsync(identity, LogExtension.Settings);
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
    /// <param name="setting">
    /// Log's settings.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        string identity, LogSetting setting)
    {
        try
        {
            if (value != null)
            {
                setting = setting ?? LogExtension.Settings;
                identity = identity.IsNotNullOrEmpty() ? identity
                    : Guid.NewGuid().ToString("N");
                bool appendLog = setting != null ? setting.AppendException : false;
                new FileService().SaveAsync(value,
                    identity, setting?.LogFolderLocation,
                    setting?.LogFileName,
                    Level.Log, setting?.LogDefaultFolderName,
                    appendLog);
            }
        }
        catch { }
    }
}