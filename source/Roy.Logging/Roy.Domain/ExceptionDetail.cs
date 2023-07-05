using Roy.Domain.Contants;
using System.Diagnostics;

namespace Roy.Domain;

/// <summary>
/// Exception details.
/// </summary>
public class ExceptionDetail : MessageDetail
{
    /// <summary>
    /// Exception message.
    /// </summary>
    public string ExceptionMessage { get; set; }
    /// <summary>
    /// Exception StackTrace.
    /// </summary>
    public string StackTrace { get; set; }
    /// <summary>
    /// list of parameters.
    /// </summary>
    public object[] ListOfParameters { get; set; }
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
    /// <param name="loadSystemInformation">
    /// Flag that determinate whether to load the system information or not.
    /// </param>
    public ExceptionDetail(Exception exception,
        Level level, string id, string message,
        object[] listOfParameters, StackFrame frame,
        bool loadSystemInformation)
        : base(level, id, message, frame, loadSystemInformation)
    {
        this.LoadObject(exception, level, listOfParameters, frame);
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
    /// <param name="listOfParameters">
    /// List of parameters.
    /// </param>
    /// <param name="frame">
    /// Stack frame containing the method calling the log.
    /// </param>
    private void LoadObject(Exception exception, Level level, 
        object[] listOfParameters, StackFrame frame)
    {
        this.ExceptionMessage = exception.Message;
        this.ListOfParameters = listOfParameters;
        this.SetExceptionTrace(exception, level);
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
}