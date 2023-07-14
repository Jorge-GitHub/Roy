using Roy.Domain.Contants;

namespace Roy.Domain.Settings.Web.EmailAspect;

/// <summary>
/// Email settings.
/// </summary>
public class EmailSetting
{
    /// <summary>
    /// User account's name.
    /// </summary>
    public string UserAccount { get; set; }
    /// <summary>
    /// User's password.
    /// </summary>
    public string UserPassword { get; set; }
    /// <summary>
    /// From email.
    /// </summary>
    public string From { get; set; }
    /// <summary>
    /// Display from value.
    /// </summary>
    public string DisplayNameFrom { get; set; }
    /// <summary>
    /// Default email's subject.
    /// </summary>
    public string DefaultEmailSubject { get; set; }
    /// <summary>
    /// Default email's body.
    /// </summary>
    public string DefaultEmailBody { get; set; }
    /// <summary>
    /// Default flag indicating whether the be send as text or HTML.
    /// By default it will be send as HTML.
    /// </summary>
    public bool DefaultIsTextBody { get; set; }
    /// <summary>
    /// Flag that indicate if the email will be send by using SSL.
    /// </summary>
    public bool EnableSSL { get; set; }
    /// <summary>
    /// Server settings.
    /// </summary>
    public ServerSetting Server { get; set; }
    /// <summary>
    /// Email receivers.
    /// </summary>
    public List<ReceiverSetting> Receivers { get; set; }
    /// <summary>
    /// Exception levels to report. If null or empty, it will email all the exceptions. 
    /// </summary>
    public List<Level> LevelsToReport { get; set; }
    /// <summary>
    /// Flag that determinate whether to disable or not sending the emails to the list.
    /// </summary>
    public bool DisableEmailSending { get; set; }
    /// <summary>
    /// Constructor.
    /// </summary>
    public EmailSetting()
    {
        this.Server= new ServerSetting();
        this.Receivers= new List<ReceiverSetting>();
        this.LevelsToReport = new List<Level>();
    }
}