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
/// Log extensions.
/// </summary>
public static class LoggerExtension
{
    /// <summary>
    /// Log the object or primitive.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    /// <param name="level">
    /// Optional: Log's level. Default value is Log.
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
    public static async Task<ProcessMessageDTO> LogAsync<TValue>(
        this TValue value, Level level = Level.Log, 
        WebApplicationHttpContext? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        return await value.LogAsync(string.Empty, string.Empty, 
            LogExtension.Settings, null, level, 
            webApplicationHttpContext, listOfParameters);
    }

    /// <summary>
    /// Log the object or primitive.
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
    /// <param name="level">
    /// Optional: Log's level. Default value is Log.
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
    public static async Task<ProcessMessageDTO> LogAsync<TValue>(
        this TValue value, StackFrame frame, Level level = Level.Log, 
        WebApplicationHttpContext? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        return await value.LogAsync(string.Empty, string.Empty, LogExtension.Settings, 
            frame, level, webApplicationHttpContext, listOfParameters);
    }

    /// <summary>
    /// Log the object or primitive.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    /// <param name="level">
    /// Optional: Log's level. Default value is Log.
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
    public static async Task<ProcessMessageDTO> LogAsync<TValue>(
        this TValue value, RoySetting settings, Level level = Level.Log, 
        WebApplicationHttpContext? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        return await value.LogAsync(string.Empty, string.Empty, settings, 
            null, level, webApplicationHttpContext, listOfParameters);
    }

    /// <summary>
    /// Log the object or primitive.
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
    /// <param name="level">
    /// Optional: Log's level. Default value is Log.
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
    public static async Task<ProcessMessageDTO> LogAsync<TValue>(
        this TValue value, string message, Level level = Level.Log,
        WebApplicationHttpContext ? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        return await value.LogAsync(message, string.Empty, LogExtension.Settings, 
            null, level, webApplicationHttpContext, listOfParameters);
    }

    /// <summary>
    /// Log the object or primitive.
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
    /// <param name="level">
    /// Optional: Log's level. Default value is Log.
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
    public static async Task<ProcessMessageDTO> LogAsync<TValue>(this TValue value,
        string message, string identity, Level level = Level.Log,
        WebApplicationHttpContext? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        return await value.LogAsync(message, identity, 
            LogExtension.Settings, null, level,
            webApplicationHttpContext, listOfParameters);
    }

    /// <summary>
    /// Log the object or primitive.
    /// </summary>
    /// <typeparam name="TValue">
    /// Value type to log.
    /// </typeparam>
    /// <param name="value">
    /// Value type to log.
    /// </param>
    /// <param name="level">
    /// Optional: Log's level. Default value is Log.
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
    /// <param name="listOfParameters">
    /// Optional: List of parameters.
    /// </param>
    /// <returns>
    /// Message returned by the logging service.
    /// </returns>
    public static async Task<ProcessMessageDTO> LogAsync<TValue>(
        this TValue value, string message, string identity, 
        RoySetting setting, StackFrame frame, Level level = Level.Log, 
        WebApplicationHttpContext? webApplicationHttpContext = null,
        params object[] listOfParameters)
    {
        InternalProcessMessage process = new InternalProcessMessage();
        try
        {
            setting = setting ?? LogExtension.Settings;
            if (value.IsNotNull() && setting.IsNotNull() 
                && setting.Log.Disable.IsNotTrue())
            {                
                identity = identity.IsNotNullOrEmpty() ? identity
                    : Guid.NewGuid().ToString("N");
                
                LogDetail detail = new LogDetail(value, level,  identity, message, 
                    frame, setting.Log.LoadInformationSettings, 
                    webApplicationHttpContext, listOfParameters);

                process = new RecordService().Save(
                    detail, setting.Log);
            }
        }
        catch (Exception ex)
        {
            process.Errors.Add(ex);
        }

        return process.ToDTO();
    }
}