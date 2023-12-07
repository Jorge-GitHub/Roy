namespace Roy.Logging.Domain.DTO.Program;

/// <summary>
/// Web application details data transfer object (DTO).
/// </summary>
public class WebApplicationDTO
{
    /// <summary>
    /// Current URL.
    /// </summary>
    public string? CurrentURL { get; set; }
    /// <summary>
    /// Current page parameters.
    /// </summary>
    public string? CurrentURLParameters { get; set; }
    /// <summary>
    /// Previous URL.
    /// </summary>
    public string? PreviousURL { get; set; }
    /// <summary>
    /// User's Host's IP.
    /// </summary>
    public string? UserHostIP { get; set; }
    /// <summary>
    /// Flag that indicates whether the application is running in a secure connection.
    /// </summary>
    public bool IsSecureConnection { get; set; }
    /// <summary>
    /// Domain's name.
    /// </summary>
    public string? DomainName { get; set; }
    /// <summary>
    /// Cookies values.
    /// </summary>
    public List<string>? CookiesValues { get; set; }
    /// <summary>
    /// Headers values.
    /// </summary>
    public List<string>? HeadersValues { get; set; }
    /// <summary>
    /// User preference languages.
    /// </summary>
    public string? UserLanguagePreferences { get; set; }
}