using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings;
using System.Diagnostics;

namespace Roy.Logging;

/// <summary>
/// Log extensions.
/// </summary>
public static class LoggerExtension
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
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static void LogAsync<TValue>(this TValue value, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        value.LogAsync(string.Empty, string.Empty, LogExtension.Settings, 
            null, webApplicationHttpContext);
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
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static void LogAsync<TValue>(this TValue value, StackFrame frame, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        value.LogAsync(string.Empty, string.Empty, LogExtension.Settings, 
            frame, webApplicationHttpContext);
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
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        RoySetting settings, WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        value.LogAsync(string.Empty, string.Empty, settings, null,
            webApplicationHttpContext);
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
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        string message, WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        value.LogAsync(message, string.Empty, LogExtension.Settings, null,
            webApplicationHttpContext);
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
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        string message, string identity, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        value.LogAsync(message, identity, LogExtension.Settings, null,
            webApplicationHttpContext);
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
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void LogAsync<TValue>(this TValue value,
        string message, string identity,  RoySetting setting, StackFrame frame,
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        try
        {
            setting = setting ?? LogExtension.Settings;
            if (value != null && setting != null && !setting.Log.Disable)
            {                
                identity = identity.IsNotNullOrEmpty() ? identity
                    : Guid.NewGuid().ToString("N");
                
                LogDetail detail = new LogDetail(value, identity, 
                    message, frame, setting.Log.LogSettings, webApplicationHttpContext);

                new RecordService().SaveAsync(
                    detail, setting.Log);
            }
        }
        catch { }
    }
}