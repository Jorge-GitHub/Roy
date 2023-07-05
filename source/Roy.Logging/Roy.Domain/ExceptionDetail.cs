using Avalon.Base.Extension.Types;
using Roy.Domain.Application;
using Roy.Domain.Contants;
using System.Diagnostics;
using System.Reflection;

namespace Roy.Domain;

/// <summary>
/// Exception details.
/// </summary>
public class ExceptionDetail
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
    /// Exception message.
    /// </summary>
    public string ExceptionMessage { get; set; }
    /// <summary>
    /// Custom message.
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// Exception StackTrace.
    /// </summary>
    public string StackTrace { get; set; }
    /// <summary>
    /// Assembly that the current code is running from.
    /// </summary>
    public string AssemblyLocation { get; set; }
    /// <summary>
    /// list of parameters.
    /// </summary>
    public object[] ListOfParameters { get; set; }
    /// <summary>
    /// Machine/Server information.
    /// </summary>
    public Machine MachineInformation { get; set; }
    /// <summary>
    /// Stack frame making the call to the method.
    /// </summary>
    public Frame StackFrame { get; set; }
    /// <summary>
    /// Full exception error.
    /// </summary>
    public Exception ExceptionTrace { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="level">
    /// Exception's level
    /// If level is Trace or Debug, it will add the full exception.
    /// </param>
    /// <param name="id">
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
    public ExceptionDetail(Exception exception,
        Level level, string id, string message,
        object[] listOfParameters, StackFrame stackFrame)
    {
        this.LoadObject(exception, level, id, message, 
            listOfParameters, stackFrame);
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="level">
    /// Exception's level.
    /// </param>
    /// <param name="id">
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
    private void LoadObject(Exception exception,
        Level level, string id, string message,
        object[] listOfParameters, StackFrame frame)
    {
        this.Date = DateTime.Now;
        this.Level = level;
        this.Id = id.IsNotNullOrEmpty() ? id : Guid.NewGuid().ToString("N");
        this.ExceptionMessage = exception.Message;
        this.Message = message;
        this.ListOfParameters = listOfParameters;
        this.SetExceptionTrace(exception, level);
        try
        {
            this.SetAssemblyLocation();
            this.MachineInformation = new Machine(true);
            this.StackFrame = new Frame(frame);
        }
        catch { }
    }

    /// <summary>
    /// Set the exception trace.
    /// </summary>
    /// <param name="exception">
    /// Exception.
    /// </param>
    /// <param name="level">
    /// Exception's level
    /// </param>
    private void SetExceptionTrace(Exception exception, Level level)
    {
        if (level.Equals(Level.Trace) || level.Equals(Level.Debug))
        {
            this.ExceptionTrace = exception;
            this.StackTrace = exception.StackTrace ?? string.Empty;
        }
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