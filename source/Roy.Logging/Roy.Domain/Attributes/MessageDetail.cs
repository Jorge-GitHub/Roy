using Avalon.Base.Extension.Types;
using Roy.Domain.Application;
using Roy.Domain.Contants;
using System.Diagnostics;
using System.Reflection;

namespace Roy.Domain.Attributes;

/// <summary>
/// Message's detail.
/// </summary>
public class MessageDetail
{
    /// <summary>
    /// Date time.
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Exception level.
    /// </summary>
    public Level Level { get; set; }
    /// <summary>
    /// Id.
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Log message.
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// Assembly that the current code is running from.
    /// </summary>
    public string AssemblyLocation { get; set; }
    /// <summary>
    /// Machine/Server information.
    /// </summary>
    public Machine MachineInformation { get; set; }
    /// <summary>
    /// Stack frame making the call to the method.
    /// </summary>
    public Frame StackFrame { get; set; }

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
    /// <param name="loadSystemInformation">
    /// Flag that determinate whether to load the system information or not.
    /// </param>
    public MessageDetail(Level level,
        string id, string message, StackFrame frame,
        bool loadSystemInformation)
    {
        LoadObject(level, id, message, frame, loadSystemInformation);
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
    /// <param name="loadSystemInformation">
    /// Flag that determinate whether to load the system information or not.
    /// </param>
    private void LoadObject(Level level, string id, string message,
        StackFrame frame, bool loadSystemInformation)
    {
        Date = DateTime.Now;
        Level = level;
        Id = id.IsNotNullOrEmpty() ? id : Guid.NewGuid().ToString("N");
        Message = message;
        if (loadSystemInformation)
        {
            LoadSystemInformation(frame);
        }
    }

    /// <summary>
    /// Load the system information.
    /// </summary>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    private void LoadSystemInformation(StackFrame frame)
    {
        try
        {
            SetAssemblyLocation();
            MachineInformation = new Machine(true);
            StackFrame = new Frame(frame);
        }
        catch { }
    }

    /// <summary>
    /// Set assembly location.
    /// </summary>
    private void SetAssemblyLocation()
    {
        string location = Assembly.GetExecutingAssembly().Location;
        if (location.IsNotNullOrEmpty())
        {
            AssemblyLocation = Path.GetDirectoryName(location);
        }
    }
}