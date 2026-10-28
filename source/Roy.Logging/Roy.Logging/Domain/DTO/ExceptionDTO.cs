using Roy.Logging.Json;
using System.Text.Json.Serialization;

namespace Roy.Logging.Domain.DTO;

/// <summary>
/// Exception details data transfer object (DTO).
/// </summary>
public class ExceptionDTO : MessageDTO
{
    /// <summary>
    /// Exception message.
    /// </summary>
    public string? ExceptionMessage { get; set; }
    /// <summary>
    /// Exception StackTrace.
    /// </summary>
    public string? StackTrace { get; set; }
    /// <summary>
    /// Full exception error.
    /// </summary>
    [JsonConverter(typeof(ExceptionJsonConverter))]
    public Exception? ExceptionTrace { get; set; }
}
