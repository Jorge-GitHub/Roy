using Roy.Domain.Contants;
using System.Diagnostics;

namespace Roy.Domain.Attributes;

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
    /// <param name="loadSystemInformation">
    /// Flag that determinate whether to load the system information or not.
    /// </param>
    public LogDetail(object LogValue,
        Level level, string id, string message,
        StackFrame frame, bool loadSystemInformation)
        : base(level, id, message, frame, loadSystemInformation)
    {
        LoadObject(LogValue);
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