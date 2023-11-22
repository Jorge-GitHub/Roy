namespace Roy.Logging.Domain.Settings.Attributes;

/// <summary>
/// Log settings.
/// </summary>
public class LogSetting
{
    /// <summary>
    /// Flag that determinate whether to log the method information or not
    /// on the exception/log logging.
    /// </summary>
    public bool LogMethodInformation { get; set; }
    /// <summary>
    /// Flag that determinate whether to load the browser information or not.
    /// </summary>
    public bool LogBrowserInformation { get; set; }
    /// <summary>
    /// Flag that determinate whether to load the application information or not.
    /// </summary>
    public bool LogApplicationInformation { get; set; }
    /// <summary>
    /// Flag that determinate whether to load the machine/server information or not.
    /// </summary>
    public bool LogMachineInformation { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public LogSetting()
    {
        this.LogMethodInformation = true;
    }
}