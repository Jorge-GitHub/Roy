﻿using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using Roy.Domain.Settings;
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
        exception.SaveAsync(LogExtension.Settings.Exception.DefaultLevel,
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
        exception.SaveAsync(LogExtension.Settings.Exception.DefaultLevel,
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
        RoySetting settings)
    {
        exception.SaveAsync(settings.Exception.DefaultLevel,
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
        Level level, RoySetting settings)
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
    /// <param name="message">
    /// Custom message.
    /// </param>
    public static async void SaveAsync(this Exception exception,
        string message)
    {
        exception.SaveAsync(Level.Error, message, string.Empty,
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
    public static async void SaveAsync(this Exception exception,
        string message, string identity)
    {
        exception.SaveAsync(Level.Error, message, identity,
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
        string message, string identity, 
        params object[] listOfParameters)
    {
        exception.SaveAsync(Level.Error, message, identity,
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
        string message, string identity,
        StackFrame frame, params object[] listOfParameters)
    {
        exception.SaveAsync(Level.Error, message, identity,
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
        Level level, string message, string identity, 
        RoySetting setting, StackFrame frame, 
        params object[] listOfParameters)
    {
        try
        {
            setting = setting ?? LogExtension.Settings;
            if (exception != null && setting != null && setting.Log.Enable)
            {                
                bool appendLog = setting != null ? setting.Exception.Append : false;
                bool logSystemInformation = setting != null ? 
                    setting.Exception.LoadSystemInformation : true;
                
                ExceptionDetail exceptionDetail = new ExceptionDetail(exception,
                    level, identity, message, listOfParameters, 
                    frame, logSystemInformation);
                
                new FileService().SaveAsync(exceptionDetail,
                    exceptionDetail.Id,
                    setting?.Exception.FolderLocation,
                    setting?.Exception.FileName,
                    level, setting?.Exception.DefaultFolderName,
                    appendLog);
            }
        }
        catch { }
    }
}