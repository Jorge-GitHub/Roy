using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings.Attributes;
using System.Diagnostics;

namespace Roy.Logging.Domain.Attributes;

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
    public ExceptionDetail(Exception exception,
        Level level, string id, string message,
        StackFrame frame,
        InformationSetting logSettings, 
        WebApplicationHttpContext webApplicationHttpContext,
        object[] listOfParameters)
        : base(level, id, message, frame, logSettings, MessageType.Exception,
            webApplicationHttpContext, listOfParameters)
    {
        this.LoadObject(exception, level, listOfParameters);
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
    private void LoadObject(Exception exception, Level level,
        object[] listOfParameters)
    {
        this.ExceptionMessage = exception.Message;
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