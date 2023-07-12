using Roy.Domain.Contants;
using Roy.Domain.Settings.Web.EmailAspect;

namespace Roy.Domain.Settings.Attributes;

/// <summary>
/// Settings.
/// </summary>
public class Setting
{
    /// <summary>
    /// Folder Location to save the application's exceptions/logs.
    /// </summary>
    /// <remarks>
    /// If null or empty, the folder to use will be the running 
    /// assembly location + exceptions/log.
    /// </remarks>
    public string? FolderLocation { get; set; }
    /// <summary>
    /// File to save the application's exceptions/logs.
    /// </summary>
    /// <remarks>
    /// If null or empty, each exception/logs will have its own file.
    /// The file's name will be the exception/logs's Id.
    /// </remarks>
    public string? FileName { get; set; }
    /// <summary>
    /// Folder's name to use in case the folder location is null or empty.
    /// Default value is "exceptions" for exceptions and "logs" for logs.
    /// </summary>
    /// <remarks>
    /// If folder location is null, it will save 
    /// the file within the assembly folder.
    /// </remarks>
    public string? DefaultFolderName { get; set; }
    /// <summary>
    /// Default notification level.
    /// </summary>
    public Level DefaultLevel { get; set; } = Level.Error;
    /// <summary>
    /// Flag that determinate whether to append or not the exception/log.
    /// </summary>
    public bool Append { get; set; }
    /// <summary>
    /// Determinate whether the exception/log  is enable or not.
    /// You can disable all the exception/logs' logging by settings this value to true.
    /// </summary>
    public bool Disable { get; set; }
    /// <summary>
    /// Flag that determinate whether to load the system information or not
    /// on the exception/log logging.
    /// </summary>
    public bool LoadSystemInformation { get; set; } = true;
    /// <summary>
    /// Email settings.
    /// </summary>
    public List<EmailSetting> Emails { get; set; } = new List<EmailSetting>();
}