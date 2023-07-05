using Roy.Domain;
using Roy.Domain.Contants;
using Roy.Logging.Helpers;
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
    public static async void SaveAsync(this Exception exception)
    {
        exception.SaveAsync(LogExtension.Settings.DefaultExceptionLevel,
            string.Empty, string.Empty, LogExtension.Settings, null, null);
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
    public static async void SaveAsync(this Exception exception, StackFrame frame)
    {
        exception.SaveAsync(LogExtension.Settings.DefaultExceptionLevel,
            string.Empty, string.Empty, LogExtension.Settings, frame, null);
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
    public static async void SaveAsync(this Exception exception,
        LogSetting settings)
    {
        exception.SaveAsync(settings.DefaultExceptionLevel,
            string.Empty, string.Empty, settings, null, null);
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
    public static async void SaveAsync(this Exception exception, Level level)
    {
        exception.SaveAsync(level, string.Empty, string.Empty,
            LogExtension.Settings, null, null);
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
    public static async void SaveAsync(this Exception exception, Level level,
        StackFrame frame)
    {
        exception.SaveAsync(level, string.Empty, string.Empty,
            LogExtension.Settings, frame, null);
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
    public static async void SaveAsync(this Exception exception,
        Level level, LogSetting settings)
    {
        exception.SaveAsync(level, string.Empty, string.Empty,
            settings, null, null);
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
    public static async void SaveAsync(this Exception exception,
        string identity, string message)
    {
        exception.SaveAsync(Level.Error, identity, message,
            LogExtension.Settings, null);
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
    public static async void SaveAsync(this Exception exception,
        string identity, string message,
        params object[] listOfParameters)
    {
        exception.SaveAsync(Level.Error, identity, message,
            LogExtension.Settings, null, listOfParameters);
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
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        string identity, string message,
        StackFrame frame, params object[] listOfParameters)
    {
        exception.SaveAsync(Level.Error, identity, message,
            LogExtension.Settings, frame, listOfParameters);
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
        Level level, string identity, string message,
        LogSetting setting, StackFrame frame, 
        params object[] listOfParameters)
    {
        try
        {
            if (exception != null)
            {
                setting = setting ?? LogExtension.Settings;
                
                bool appendLog = setting != null ? setting.AppendException : false;
                bool logSystemInformation = setting != null ? 
                    setting.ExceptionLogSystemInformation: true;
                
                ExceptionDetail exceptionDetail = new ExceptionDetail(exception,
                    level, identity, message, listOfParameters, 
                    frame, logSystemInformation);
                
                new FileService().SaveAsync(exceptionDetail,
                    exceptionDetail.Id,
                    setting?.ExceptionFolderLocation,
                    setting?.ExceptionFileName,
                    level, setting?.ExceptionDefaultFolderName,
                    appendLog);
            }
        }
        catch { }
    }
}