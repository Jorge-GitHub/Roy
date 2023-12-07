namespace Roy.Logging.Domain.DTO;

/// <summary>
/// Log's detail data transfer object (DTO).
/// </summary>
public class LogDTO : MessageDTO
{
    /// <summary>
    /// Log value.
    /// </summary>
    public object? LogValue { get; set; }
}