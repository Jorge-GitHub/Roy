using Microsoft.AspNetCore.Http;
using Roy.Logging.Domain.Program;

namespace Roy.Logging.MVC.Domain.Program;

/// <summary>
/// MVC web application details.
/// </summary>
public class MVCWebApplication : WebApplication
{
    /// <summary>
    /// constructor.
    /// </summary>
    public MVCWebApplication()
    {
    }

    /// <summary>
    /// Constructor that builds the object.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    /// <param name="loadBrowserInformation">
    /// Flag that indicates whether to load the browser information or not.
    /// </param>
    public MVCWebApplication(HttpContext context, bool loadBrowserInformation) 
        : base(true)
    {
        this.loadObject(context, loadBrowserInformation);
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    /// <param name="loadBrowserInformation">
    /// Flag that indicates whether to load the browser information or not.
    /// </param>
    private void loadObject(HttpContext context, bool loadBrowserInformation)
    {
        try
        {
            var userAgent = context.Request.Headers.UserAgent;
            if (loadBrowserInformation)
            {
                this.WebBrowserInformation = this.LoadBrowserInformation(context);
            }
        }
        catch
        {
            this.FailedToLoad = true;
        }
    }

    /// <summary>
    /// Load browser information.
    /// </summary>
    /// <param name="loadBrowserInformation">
    /// Flag that indicates whether to load the browser information or not.
    /// </param>
    private WebBrowser LoadBrowserInformation(HttpContext context)
    {
        try
        {
            WebBrowser browser = new WebBrowser();
            //browser.BrowserID = context.Request
            return browser;
        }
        catch
        {
        }

        return null;
    }
}
