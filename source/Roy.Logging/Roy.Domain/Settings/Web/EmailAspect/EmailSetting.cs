using Roy.Domain.Contants;

namespace Roy.Domain.Settings.Web.EmailAspect;

/// <summary>
/// Email settings.
/// </summary>
public class EmailSetting
{
    /// <summary>
    /// Default email's subject.
    /// </summary>
    public string DefaultEmailSubject { get; set; }
    /// <summary>
    /// Email's body.
    /// </summary>
    public string DefaultEmailBody { get; set; }
    /// <summary>
    /// Server settings.
    /// </summary>
    public ServerSetting Server { get; set; }
    /// <summary>
    /// Email receivers.
    /// </summary>
    public List<ReceiverSetting> Receivers { get; set; } = new List<ReceiverSetting>();
    /// <summary>
    /// Exception levels to report. If null or empty, it will email all the exceptions. 
    /// </summary>
    public List<Level> LevelsToReport { get; set; } = new List<Level>();
}