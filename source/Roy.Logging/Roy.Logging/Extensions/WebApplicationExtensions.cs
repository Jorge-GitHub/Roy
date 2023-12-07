using Roy.Logging.Domain.Program;

namespace Roy.Logging.Extensions;

/// <summary>
/// Web application extensions.
/// </summary>
internal static class WebApplicationExtensions
{
    /// <summary>
    /// to web application details.
    /// </summary>
    /// <param name="webApplicationContext">
    /// Web application HttpContext.
    /// </param>
    /// <returns>
    /// Web application details.
    /// </returns>
    public static WebApplication ToWebApplication(
        this WebApplicationHttpContext webApplicationContext)
    {
        WebApplication application = new WebApplication(true);
        application.CurrentURLParameters = webApplicationContext.CurrentURLParameters;
        application.CurrentURL = webApplicationContext.CurrentURL;
        application.PreviousURL = webApplicationContext.PreviousURL;
        application.UserHostIP = webApplicationContext.UserHostIP;
        application.IsSecureConnection = webApplicationContext.IsSecureConnection;
        application.UserDomainName = webApplicationContext.UserDomainName;
        application.UserLanguagePreferences = webApplicationContext.UserLanguagePreferences;
        application.CookiesValues = webApplicationContext.CookiesValues;
        application.HeadersValues = webApplicationContext.HeadersValues;
        application.FailedToLoad = webApplicationContext.FailedToLoad;

        return application;
    }
}