namespace Roy.Logging.Domain.Program;

/// <summary>
/// Web application HttpContext.
/// </summary>
public class WebApplicationHttpContext
{
    /// <summary>
    /// Current URL.
    /// </summary>
    public string CurrentURL { get; set; }
    /// <summary>
    /// Current page parameters.
    /// </summary>
    public string CurentURLParameters { get; set; }
    /// <summary>
    /// Previous URL.
    /// </summary>
    public string PreviousURL { get; set; }
    /// <summary>
    /// User's Host's IP.
    /// </summary>
    public string UserHostIP { get; set; }
    /// <summary>
    /// Flag that indicates whether the application is running in a secure connection.
    /// </summary>
    public bool IsSecureConnection { get; set; }
    /// <summary>
    /// Network domain name associated with the current user.
    /// </summary>
    public string UserDomainName { get; set; }
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
    /// Flag that determinate whether the object failed to load.
    /// </summary>
    public bool FailedToLoad { get; set; }
}
