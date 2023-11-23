using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings;
using System.Diagnostics;

namespace Roy.Logging;

/// <summary>
/// Exception extensions.
/// </summary>
public static class ExceptionExtension
{
    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        exception.SaveAsync(LogExtension.Settings.Exception.DefaultLevel,
            string.Empty, string.Empty, LogExtension.Settings, null, null,
            webApplicationHttpContext, null);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception, StackFrame frame,
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        exception.SaveAsync(LogExtension.Settings.Exception.DefaultLevel,
            string.Empty, string.Empty, LogExtension.Settings, frame, null,
            webApplicationHttpContext, null);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="settings">
    /// Settings.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        RoySetting settings, WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        exception.SaveAsync(settings.Exception.DefaultLevel,
            string.Empty, string.Empty, settings, null, null, 
            webApplicationHttpContext, null);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="level">
    /// Exception's level
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception, Level level,
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        exception.SaveAsync(level, string.Empty, string.Empty,
            LogExtension.Settings, null, null, webApplicationHttpContext, null);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="level">
    /// Exception's level
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception, Level level,
        StackFrame frame, WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        exception.SaveAsync(level, string.Empty, string.Empty,
            LogExtension.Settings, frame, null, webApplicationHttpContext, null);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="level">
    /// Exception's level
    /// </param>
    /// <param name="settings">
    /// Settings.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        Level level, RoySetting settings, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        exception.SaveAsync(level, string.Empty, string.Empty,
            settings, null, webApplicationHttpContext, null);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="message">
    /// Custom message.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        string message, WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        exception.SaveAsync(Level.Error, message, string.Empty,
            LogExtension.Settings, null, webApplicationHttpContext, null);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="identity">
    /// Exception's Id.
    /// </param>
    /// <param name="message">
    /// Custom message.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        string message, string identity, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        exception.SaveAsync(Level.Error, message, identity,
            LogExtension.Settings, null, webApplicationHttpContext, null);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="identity">
    /// Exception's Id.
    /// </param>
    /// <param name="message">
    /// Custom message.
    /// </param>
    /// <param name="listOfParameters">
    /// List of parameters.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        string message, string identity,
        WebApplicationHttpContext? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        exception.SaveAsync(Level.Error, message, identity,
            LogExtension.Settings, null, webApplicationHttpContext, listOfParameters);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="identity">
    /// Exception's Id.
    /// </param>
    /// <param name="message">
    /// Custom message.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    /// <param name="listOfParameters">
    /// List of parameters.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        string message, string identity,
        StackFrame frame, WebApplicationHttpContext? webApplicationHttpContext = null, 
        params object[] listOfParameters)
    {
        exception.SaveAsync(Level.Error, message, identity,
            LogExtension.Settings, frame, webApplicationHttpContext, listOfParameters);
    }

    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="level">
    /// Exception's level
    /// </param>
    /// <param name="identity">
    /// Exception's Id.
    /// </param>
    /// <param name="message">
    /// Custom message.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    /// <param name="listOfParameters">
    /// List of parameters.
    /// </param>
    /// <param name="setting">
    /// Log's settings.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        Level level, string message, string identity, 
        RoySetting setting, StackFrame frame,
        WebApplicationHttpContext? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        try
        {
            setting = setting ?? LogExtension.Settings;
            if (exception != null && setting != null && !setting.Log.Disable)
            {                
                ExceptionDetail exceptionDetail = new ExceptionDetail(exception,
                    level, identity, message, listOfParameters, 
                    frame, setting.Exception.LogSettings);

                new RecordService().SaveAsync(exceptionDetail, 
                    setting.Exception);
            }
        }
        catch { }
    }
}