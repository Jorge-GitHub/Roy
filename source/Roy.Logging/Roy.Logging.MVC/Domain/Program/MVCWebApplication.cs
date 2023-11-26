using Microsoft.AspNetCore.Http;
using Roy.Logging.Domain.Program;

namespace Roy.Logging.MVC.Domain.Program;

/// <summary>
/// MVC web application details.
/// </summary>
public class MVCWebApplication : WebApplication
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public MVCWebApplication() { }

    /// <summary>
    /// Constructor that builds the object.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    public MVCWebApplication(HttpContext context) : base(true) { } 
}
