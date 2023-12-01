using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings.Attributes;
using System.Diagnostics;

namespace Roy.Logging.Domain.Attributes;

/// <summary>
/// Log's detail.
/// </summary>
public class LogDetail : MessageDetail
{
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
    /// <param name="logSettings">
    /// Log settings.
    /// </param>
    /// <param name="webApplicationHttpContext">
    /// Web application HttpContext details.
    /// </param>
    /// <param name="listOfParameters">
    /// Optional: List of parameters.
    /// </param>
    public LogDetail(object LogValue, Level level, string id, 
        string message, StackFrame frame, InformationSetting logSettings,
        WebApplicationHttpContext webApplicationHttpContext,
        object[] listOfParameters)
        : base(level, id, message, frame, logSettings, MessageType.Log,
            webApplicationHttpContext, listOfParameters)
    {
        this.LoadObject(LogValue);
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
    private void LoadObject(object logValue)
    {
        LogValue = logValue;
    }
}