using Microsoft.AspNetCore.Http;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings;
using Roy.Logging.MVC.Extensions;

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
    /// <param name="exception">
    /// Exception thrown by the request.
    /// </param>
    public static async Task LogErrorAsync(HttpContext context, Exception exception)
    {
        if (exception != null)
        {
            try
            {
                WebApplicationHttpContext? webAppContext = null;
                if (context != null && LogExtension.Settings.Exception
                    .LoadInformationSettings.LogApplicationInformation)
                {
                    webAppContext = context.ToWebApplicationHttpContext();
                }
                await exception.SaveAsync(webAppContext);
            }
            catch { }
        }
    }
}
