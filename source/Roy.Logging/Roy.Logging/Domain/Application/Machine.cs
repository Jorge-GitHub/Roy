namespace Roy.Logging.Domain.Application;

/// <summary>
/// Machine environment.
/// </summary>
public class Machine
{
    /// <summary>
    /// .NET CLR Version.
    /// </summary>
    public string CLRVersion { get; set; }
    /// <summary>
    /// Domain name.
    /// </summary>
    public string DomainName { get; set; }
    /// <summary>
    /// Machine's name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Operative system version.
    /// </summary>
    public string OperativeSystemVersion { get; set; }
    /// <summary>
    /// User account name.
    /// </summary>
    public string UserAccountName { get; set; }
    /// <summary>
    /// Operative system.
    /// </summary>
    public string OperativeSystem { get; set; }

    /// <summary>
    /// constructor.
    /// </summary>
    public Machine()
    {
    }

    /// <summary>
    /// Constructor that builds the object.
    /// </summary>
    /// <param name="load">
    /// Flag that indicates whether to load/build the object or not.
    /// </param>
    public Machine(bool load)
    {
        this.loadObject(load);
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="load">
    /// Flag that indicates whether to load/build the object or not.
    /// </param>
    private void loadObject(bool load)
    {
        if (load)
        {
            this.CLRVersion = Environment.Version.ToString();
            this.DomainName = Environment.UserDomainName;
            this.Name = Environment.MachineName;
            this.OperativeSystemVersion = Environment.OSVersion.VersionString;
            this.UserAccountName = Environment.UserName;
            this.OperativeSystem = this.GetOperativeSystem();
        }
    }

    /// <summary>
    /// Get the operative system.
    /// </summary>
    /// <returns>
    /// Operative system if is a Windows or Linux machine. 
    /// Otherwise returns "Other".
    /// </returns>
    private string GetOperativeSystem()
    {
        if (OperatingSystem.IsWindows())
        {
            return "Windows";
        }
        if (OperatingSystem.IsLinux())
        {
            return "Linux";
        }
        return "Other";
    }
}