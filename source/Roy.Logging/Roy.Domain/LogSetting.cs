using Roy.Domain.Contants;

namespace Roy.Domain;

/// <summary>
/// Log settings.
/// </summary>
public class LogSetting
{
    /// <summary>
    /// Folder Location to save the application's exceptions.
    /// </summary>
    /// <remarks>
    /// If null or empty, the folder to use will be the running 
    /// assembly location + exceptions.
    /// </remarks>
    public string? ExceptionFolderLocation { get; set; }
    /// <summary>
    /// File to save the application's exceptions.
    /// </summary>
    /// <remarks>
    /// If null or empty, each exception will have its own file.
    /// The file's name will be the exception's Id.
    /// </remarks>
    public string? ExceptionFileName { get; set; }
    /// <summary>
    /// Exceptions' folder's name to use in case the folder location is null or empty.
    /// Default value is "exceptions".
    /// </summary>
    /// <remarks>
    /// If folder location is null, it will save 
    /// the file within the assembly folder.
    /// </remarks>
    public string? ExceptionDefaultFolderName { get; set; } = "exceptions";
    /// <summary>
    /// Default exception level
    /// </summary>
    public Level DefaultExceptionLevel { get; set; } = Level.Error;
    /// <summary>
    /// Flag that determinate whether to append or not the exception.
    /// </summary>
    public bool AppendException { get; set; }
    /// <summary>
    /// Folder Location to save the application's logs.
    /// </summary>
    /// <remarks>
    /// If null or empty, the folder to use will be the running 
    /// assembly location + logs.
    /// </remarks>
    public string? LogFolderLocation { get; set; }
    /// <summary>
    /// Logs' folder's name to use in case the folder location is null or empty.
    /// Default value is "logs".
    /// </summary>
    /// <remarks>
    /// If folder location is null, it will save 
    /// the file within the assembly folder.
    /// </remarks>
    public string? LogDefaultFolderName { get; set; } = "logs";
    /// <summary>
    /// File to save the application's logs.
    /// </summary>
    /// <remarks>
    /// If null or empty, each log will have its own file.
    /// The file's name will be the log's Id.
    /// </remarks>
    public string? LogFileName { get; set; }
    /// <summary>
    /// Flag that determinate whether to append or not the log.
    /// </summary>
    public bool AppendLog { get; set; }
}