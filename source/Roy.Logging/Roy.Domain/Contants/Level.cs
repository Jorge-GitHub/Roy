namespace Roy.Domain.Contants;

/// <summary>
/// Exception level.
/// </summary>
public enum Level
{
    None,
    /// <summary>
    /// Logs that contain the most detailed messages. 
    /// These messages may contain sensitive application data.
    /// The full exception will be logged.
    /// </summary>
    Trace,
    /// <summary>
    /// Logs that contain the most detailed messages. 
    /// These messages may contain sensitive application data.
    /// The full exception will be logged.
    /// </summary>
    Debug,
    Emergency,
    Alert,
    Critical,
    Error,
    Warning,
    Notice,
    Information,
    Log
}