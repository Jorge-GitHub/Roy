using Avalon.Base.Extension.Types;
using Roy.Domain.Application;
using Roy.Domain.Contants;
using System.Diagnostics;
using System.Reflection;

namespace Roy.Domain;

public class LogDetail
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
    /// Log value.
    /// </summary>
    public object LogValue { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="LogValue">
    /// Log value.
    /// </param>
    /// <param name="level">
    /// Log level.
    /// </param>
    /// <param name="id">
    /// Id.
    /// </param>
    /// <param name="message">
    /// Message.
    /// </param>
    /// <param name="frame">
    /// Stack frame. For diagnostics purposes.
    /// </param>
    public LogDetail(object LogValue,
        Level level, string id, string message,
        StackFrame frame)
    {
        this.LoadObject(LogValue, level, id, message, frame);
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="logValue">
    /// Value to log.
    /// </param>
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
    private void LoadObject(object logValue, Level level, 
        string id, string message, StackFrame frame)
    {
        this.Date = DateTime.Now;
        this.LogValue = logValue;
        this.Level = level;
        this.Id = id.IsNotNullOrEmpty() ? id : Guid.NewGuid().ToString("N");
        this.Message = message;
        try
        {
            this.SetAssemblyLocation();
            this.MachineInformation = new Machine(true);
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
