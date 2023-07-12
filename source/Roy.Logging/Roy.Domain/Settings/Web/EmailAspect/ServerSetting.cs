namespace Roy.Domain.Settings.Web.EmailAspect;

/// <summary>
/// Email server settings.
/// </summary>
public class ServerSetting
{
    /// <summary>
    /// From email.
    /// </summary>
    public string FromMail { get; set; }
    /// <summary>
    /// Display from value.
    /// </summary>
    public string FromDisplayName { get; set; }
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
    public int Port { get; set; } = 587;
}