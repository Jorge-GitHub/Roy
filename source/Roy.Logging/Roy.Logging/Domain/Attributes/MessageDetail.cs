using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings.Attributes;
using System.Diagnostics;
using System.Reflection;

namespace Roy.Logging.Domain.Attributes;

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
    public MessageDetail(Level level,
        string id, string message, StackFrame frame,
        LogSetting logSettings)
    {
        this.LoadObject(level, id, message, frame, logSettings);
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
    /// <param name="logSettings">
    /// Log settings.
    /// </param>
    private void LoadObject(Level level, string id, string message,
        StackFrame frame, LogSetting logSettings)
    {
        this.Date = DateTime.Now;
        this.Level = level;
        this.Id = id.IsNotNullOrEmpty() ? id : Guid.NewGuid().ToString("N");
        this.Message = message;
        this.LoadInformation(logSettings, frame);
    }

    /// <summary>
    /// Load the system information.
    /// </summary>
    /// <param name="logSettings">
    /// Log settings.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    private void LoadInformation(LogSetting logSettings, StackFrame frame)
    {
        try
        {
            this.SetAssemblyLocation();
            if (logSettings.LogBrowserInformation)
            {
                
            }
            if (logSettings.LogApplicationInformation)
            {

            }
            if (logSettings.LogMachineInformation)
            {
                this.MachineInformation = new Machine(true);
            }
            if (logSettings.LogMethodInformation)
            {
                this.StackFrame = new Method(frame);
            }
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
            this.AssemblyLocation = Path.GetDirectoryName(location);
        }
    }
}