using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings.Web.APIAspect;
using Roy.Logging.Domain.Settings.Web.EmailAspect;

namespace Roy.Logging.Domain.Settings.Attributes;

/// <summary>
/// Settings.
/// </summary>
public class IssueSetting
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
    public Level DefaultLevel { get; set; }
    /// <summary>
    /// Flag that determinate whether to append or not the exception/log.
    /// </summary>
    public bool Append { get; set; }
    /// <summary>
    /// Determinate whether the exception/log is enable or not.
    /// You can disable all the exception/logs' logging by settings this value to true.
    /// </summary>
    public bool Disable { get; set; }
    /// <summary>
    /// Flag that determinate whether to save the log in a file or not.
    /// The default value is true.
    /// </summary>
    public bool SaveLogOnFile { get; set; }
    /// <summary>
    /// Flag that determinate whether to save the issue on the (Windows/Linux) event system.
    /// </summary>
    public bool SaveIssueOnEventSystem { get; set; }
    /// <summary>
    /// Email settings.
    /// You can send emails to multiple people based on multiple Level 
    /// conditions, such as Error or Trace.
    /// </summary>
    public List<EmailSetting> Emails { get; set; }
    /// <summary>
    /// APIs settings.
    /// You can call multiple APIs based on multiple Level conditions, such as Error or Trace.
    /// </summary>
    public List<APISetting> APIs { get; set; }
    /// <summary>
    /// Logs to save on a file. If null or empty, it will log/save any issue.
    /// You can filter the values issues types that you do not want to log by adding values to this list.
    /// </summary>
    public List<Level> LevelsToSaveOnFile { get; set; }
    /// <summary>
    /// Logs to log into the system event log. If null or empty, it will log/save any issue.
    /// You can filter the values issues types that you do not want to log by adding values to this list.
    /// </summary>
    public List<Level> LevelsToLogOnSystemEvent { get; set; }
    /// <summary>
    /// Load information settings. You can load server, application, method, 
    /// and web application (MVC only) information.
    /// </summary>
    public InformationSetting LoadInformationSettings { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public IssueSetting()
    {
        this.Emails = new List<EmailSetting>();
        this.APIs = new List<APISetting>();
        this.LoadInformationSettings = new InformationSetting();
        this.DefaultLevel = Level.Error;
        this.LevelsToSaveOnFile = new List<Level>();
        this.LevelsToLogOnSystemEvent = new List<Level>();
    }
}