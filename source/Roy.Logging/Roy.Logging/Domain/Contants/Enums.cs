namespace Roy.Logging.Domain.Contants;

/// <summary>
/// Exception/log level.
/// </summary>
public enum Level
{
    None = 0,
    /// <summary>
    /// Logs that contain the most detailed messages. 
    /// These messages may contain sensitive application data.
    /// The full exception will be logged.
    /// </summary>
    Trace = 1,
    /// <summary>
    /// Logs that contain the most detailed messages. 
    /// These messages may contain sensitive application data.
    /// The full exception will be logged.
    /// </summary>
    Debug = 2,
    Emergency = 3,
    Alert = 4,
    Critical = 5,
    Error = 6,
    Warning = 7,
    Notice = 8,
    Information = 9,
    Log = 10
}

/// <summary>
/// Languages.
/// </summary>
public enum Language
{
    None = 0,
    English = 1,
    Spanish = 2,
    French = 3,
    German = 4
}

/// <summary>
/// Message type.
/// </summary>
public enum MessageType
{
    None = 0,
    Exception = 1,
    Log = 2,
}