namespace Roy.Domain.Settings.Web.EmailAspect;

/// <summary>
/// Email receiver settings.
/// </summary>
public class ReceiverSetting
{
    /// <summary>
    /// To emails divided by semi-colon (";").
    /// </summary>
    public string To { get; set; }
    /// <summary>
    /// Blind courtesy copy to emails divided by semi-colon (";").
    /// </summary>
    public string BCC { get; set; }
    /// <summary>
    /// Courtesy copy emails divided by semi-colon (";").
    /// </summary>
    public string CC { get; set; }
    /// <summary>
    /// Email's subject.
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// Email's body.
    /// </summary>
    public string Body { get; set; }
    /// <summary>
    /// If message would be send as text or HTML.
    /// By default it will be send as HTML.
    /// </summary>
    public bool? IsTextBody { get; set; }
}