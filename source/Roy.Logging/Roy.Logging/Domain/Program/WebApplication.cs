using System.Numerics;

namespace Roy.Logging.Domain.Program;

/// <summary>
/// Web application details.
/// </summary>
public class WebApplication : Application
{
    /// <summary>
    /// Current URL.
    /// </summary>
    public string CurrentURL { get; set; }
    /// <summary>
    /// Current page parameters.
    /// </summary>
    public string CurrentURLParameters { get; set; }
    /// <summary>
    /// Previous URL.
    /// </summary>
    public string PreviousURL {  get; set; }
    /// <summary>
    /// User's Host's IP.
    /// </summary>
    public string UserHostIP { get; set; }
    /// <summary>
    /// Flag that indicates whether the application is running in a secure connection.
    /// </summary>
    public bool IsSecureConnection { get; set; }
    /// <summary>
    /// Domain's name.
    /// </summary>
    public string DomainName { get; set; }
    /// <summary>
    /// Cookies values.
    /// </summary>
    public List<string> CookiesValues { get; set; }
    /// <summary>
    /// Headers values.
    /// </summary>
    public List<string> HeadersValues { get; set; }
    /// <summary>
    /// User preference languages.
    /// </summary>
    public string UserLanguagePreferences { get; set; }

    /// <summary>
    /// constructor.
    /// </summary>
    public WebApplication() { }

    /// <summary>
    /// Constructor that builds the object.
    /// </summary>
    /// <param name="load">
    /// Flag that indicates whether to load/build the object or not.
    /// </param>
    public WebApplication(bool load) : base(load) { } 
}
