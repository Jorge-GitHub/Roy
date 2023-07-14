namespace Roy.Domain.Settings.Web.EmailAspect;

/// <summary>
/// Email server settings.
/// </summary>
public class ServerSetting
{
    /// <summary>
    /// Server's password.
    /// </summary>
    public string HostPassword { get; set; }
    /// <summary>
    /// Host name or IP.
    /// </summary>
    public string Host { get; set; }
    /// <summary>
    /// Email server port. Default value is 587.
    /// </summary>
    public int Port { get; set; }
    /// <summary>
    /// Email server time out. default value -1.
    /// To enable time out set this value to greater than 0.
    /// </summary>
    public int TimeOut { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ServerSetting()
    {
        this.Port = 587;
        this.TimeOut = -1;
    }
}