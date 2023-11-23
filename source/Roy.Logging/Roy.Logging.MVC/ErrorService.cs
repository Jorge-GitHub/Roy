using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Roy.Logging.Domain.Settings;
using Roy.Logging.MVC.Domain.Program;

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
    public static void LogError(HttpContext context)
    {
        if (context != null)
        {
            try
            {
                if (LogExtension.Settings.Exception.LogSettings.LogApplicationInformation)
                {
                    MVCWebApplication application = new MVCWebApplication(context);
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