using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Roy.Logging.MVC;

/// <summary>
/// Error service.
/// </summary>
internal static class ErrorService
{
    /// <summary>
    /// Log error logic.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    /// <param name="logRequestInformation">
    /// Flag that determinate whether to log the request information, 
    /// such as browser and user information.
    /// </param>
    public static void LogError(HttpContext context, bool logRequestInformation)
    {
        if (context != null)
        {
            try
            {
                if (logRequestInformation)
                {
                    var userAgent = context.Request.Headers.UserAgent;
                }
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    contextFeature.Error.SaveAsync();
                }
            }
            catch { }
        }
    }
}