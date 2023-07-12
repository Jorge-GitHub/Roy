namespace Roy.Domain.Settings.Web.EmailAspect;

/// <summary>
/// Email receiver settings.
/// </summary>
public class ReceiverSetting
{
    /// <summary>
    /// To email.
    /// </summary>
    public string ToEmail { get; set; }
    /// <summary>
    /// Email's subject.
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// Email's body.
    /// </summary>
    public string Body { get; set; }
}