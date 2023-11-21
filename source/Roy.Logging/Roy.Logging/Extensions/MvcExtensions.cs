using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace Roy.Logging.Extensions;

/// <summary>
/// MVC extensions.
/// </summary>
public static class MvcExtensions
{
    /// <summary>
    /// Load Roy logging on errors.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    public static void UseRoyExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context => {
                if (context != null)
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        contextFeature.Error.SaveAsync();
                    }                    
                }
            });
        });
    }
}