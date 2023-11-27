namespace Roy.Logging.Domain.Settings.Attributes;

/// <summary>
/// Load information settings. You can load server, application, method, 
/// and web application (MVC only) information.
/// </summary>
public class InformationSetting
{
    /// <summary>
    /// Flag that determinate whether to log the method information or not
    /// on the exception/log logging.
    /// </summary>
    public bool LogMethodInformation { get; set; }
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
    public InformationSetting()
    {
        this.LogMethodInformation = true;
    }
}