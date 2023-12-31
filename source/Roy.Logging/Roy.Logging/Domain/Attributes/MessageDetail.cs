﻿using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Extensions;
using System.Diagnostics;

namespace Roy.Logging.Domain.Attributes;

/// <summary>
/// Message's detail.
/// </summary>
public class MessageDetail
{
    /// <summary>
    /// Id.
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Date time.
    /// </summary>
    public DateTimeOffset Date { get; set; }
    /// <summary>
    /// Exception level.
    /// </summary>
    public Level Level { get; set; }
    /// <summary>
    /// Log message.
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// Machine/Server information.
    /// </summary>
    public Machine MachineInformation { get; set; }
    /// <summary>
    /// Application's information.
    /// </summary>
    public Application ApplicationInformation { get; set; }
    /// <summary>
    /// Web application's information.
    /// </summary>
    public WebApplication WebApplicationInformation { get; set; }
    /// <summary>
    /// Stack frame making the call to the method.
    /// </summary>
    public Method StackFrame { get; set; }
    /// <summary>
    /// list of parameters.
    /// </summary>
    public object[] CustomListOfParameters { get; set; }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="level">
    /// Log's level.
    /// </param>
    /// <param name="id">
    /// Id.
    /// </param>
    /// <param name="message">
    /// Message.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    /// <param name="logSettings">
    /// Log settings.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Web application HttpContext details.
    /// </param>
    /// <param name="listOfParameters">
    /// List of parameters.
    /// </param>
    public MessageDetail(Level level,
        string id, string message, StackFrame frame,
        InformationSetting logSettings, 
        WebApplicationHttpContext webApplicationHttpContext,
        object[] listOfParameters)
    {
        this.LoadObject(level, id, message, frame, logSettings,
            webApplicationHttpContext, listOfParameters);
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="level">
    /// Log's level.
    /// </param>
    /// <param name="id">
    /// Id.
    /// </param>
    /// <param name="message">
    /// Message.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    /// <param name="informationSettings">
    /// Information settings.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Web application HttpContext details.
    /// </param>
    /// <param name="listOfParameters">
    /// Optional: List of parameters.
    /// </param>
    private void LoadObject(Level level, string id, string message,
        StackFrame frame, InformationSetting informationSettings,
        WebApplicationHttpContext webApplicationHttpContext,
        object[] listOfParameters)
    {
        this.Date = DateTimeOffset.Now;
        this.Level = level;
        this.Id = id.IsNotNullOrEmpty() ? id : Guid.NewGuid().ToString("N");
        this.Message = message;
        this.CustomListOfParameters = listOfParameters;
        this.LoadInformation(informationSettings, frame, webApplicationHttpContext);
    }

    /// <summary>
    /// Load the system information.
    /// </summary>
    /// <param name="informationSettings">
    /// Information settings.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Web application HttpContext details.
    /// </param>
    private void LoadInformation(InformationSetting informationSettings, StackFrame frame,
        WebApplicationHttpContext webApplicationHttpContext)
    {
        try
        {
            if (informationSettings.LogApplicationInformation)
            {
                if (webApplicationHttpContext.IsNotNull())
                {
                    this.WebApplicationInformation = webApplicationHttpContext
                        .ToWebApplication();
                }
                else
                {
                    this.ApplicationInformation = new Application(true);
                }
            }
            if (informationSettings.LogMachineInformation)
            {
                this.MachineInformation = new Machine(true);
            }
            if (informationSettings.LogMethodInformation)
            {
                this.StackFrame = new Method(frame);
            }
        }
        catch { }
    }
}