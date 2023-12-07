using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.BooleanExtensions;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Communication;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.DTO.Communication;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings;
using Roy.Logging.Extensions.Communication;
using Roy.Logging.Services;
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(this Exception exception,
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        return await exception.SaveAsync(LogExtension.Settings.Exception.DefaultLevel,
            string.Empty, string.Empty, LogExtension.Settings, null,
            webApplicationHttpContext);
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, StackFrame frame,
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        return await exception.SaveAsync(LogExtension.Settings.Exception.DefaultLevel,
            string.Empty, string.Empty, LogExtension.Settings, frame, 
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(this Exception exception,
        RoySetting settings, WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        return await exception.SaveAsync(settings.Exception.DefaultLevel,
            string.Empty, string.Empty, settings, null, 
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, Level level, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        return await exception.SaveAsync(level, string.Empty, string.Empty,
            LogExtension.Settings, null, webApplicationHttpContext, null);
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, Level level, StackFrame frame,
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        return await exception.SaveAsync(level, string.Empty, string.Empty,
            LogExtension.Settings, frame, webApplicationHttpContext, null);
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, Level level, RoySetting settings, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        return await exception.SaveAsync(level, string.Empty, 
            string.Empty, settings, null, webApplicationHttpContext, null);
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, string message, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        return await exception.SaveAsync(Level.Error, message, string.Empty,
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
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, string message, string identity, 
        WebApplicationHttpContext? webApplicationHttpContext = null)
    {
        return await exception.SaveAsync(Level.Error, message, identity,
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
    /// <param name="listOfParameters">
    /// List of parameters.
    /// </param>
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, string message, string identity,
        WebApplicationHttpContext? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        return await exception.SaveAsync(Level.Error, message, identity,
            LogExtension.Settings, null, webApplicationHttpContext, 
            listOfParameters);
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
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    /// <param name="listOfParameters">
    /// List of parameters.
    /// </param>
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, string message, string identity,
        StackFrame frame, WebApplicationHttpContext? webApplicationHttpContext = null, 
        params object[] listOfParameters)
    {
        return await exception.SaveAsync(Level.Error, message, 
            identity, LogExtension.Settings, frame, 
            webApplicationHttpContext, listOfParameters);
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
    /// <param name="setting">
    /// Log's settings.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Optional: Web application HttpContext details.
    /// </param>
    /// <param name="listOfParameters">
    /// Optional: List of parameters.
    /// </param>
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> SaveAsync(
        this Exception exception, Level level, string message,
        string identity, RoySetting setting, StackFrame frame,
        WebApplicationHttpContext webApplicationHttpContext,
        params object[] listOfParameters)
    {
        ProcessMessage process = new ProcessMessage();
        try
        {
            setting = setting ?? LogExtension.Settings;
            if (exception.IsNotNull() && setting.IsNotNull() 
                && setting.Log.Disable.IsNotTrue())
            {                
                ExceptionDetail exceptionDetail = new ExceptionDetail(exception,
                    level, identity, message, frame, 
                    setting.Exception.LoadInformationSettings, 
                    webApplicationHttpContext, listOfParameters);

                new RecordService().Save(exceptionDetail, 
                    setting.Exception);
            }
        }
        catch (Exception ex)
        {
            process.Errors.Add(ex);
        }

        return process.ToDTO();
    }
}