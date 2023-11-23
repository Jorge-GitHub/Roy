using Avalon.Base.Extension.Types;
using Microsoft.AspNetCore.Http;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using System.Text;

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
    public MVCWebApplication(HttpContext context) 
        : base(true)
    {
        this.LoadObject(context);
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    private void LoadObject(HttpContext context)
    {
        if (context.IsNotNull())
        {
            try
            {
                if (!this.FailedToLoad)
                {
                    this.LoadWebApplicationDetails(context);
                }
            }
            catch
            {
                this.FailedToLoad = true;
            }
        }
    }

    /// <summary>
    /// Load web application details.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    private void LoadWebApplicationDetails(HttpContext context)
    {
        if (context.Request.IsNotNull())
        {
            if (context.Request.QueryString.IsNotNull())
            {
                this.CurrentURL = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path.ToString()}{context.Request.QueryString.ToString()}";
                this.CurentURLParameters = context.Request.QueryString.ToString();
            }
            if (context.Request.Headers.IsNotNull())
            {
                this.UserLanguagePreferences = context.Request.Headers["Accept-Language"]
                    .ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
                this.PreviousURL = context.Request.Headers.Referer.ToString();
                this.HeadersValues = this.GetHeadersValues(context);
            }
            if (context.Connection.IsNotNull())
            {
                this.UserHostIP = context.Connection.RemoteIpAddress.ToSafeString();
            }
            if (context.Request.Host.IsNotNull())
            {
                this.DomainName = context.Request.Host.Value;
            }
            this.IsSecureConnection = context.Request.IsHttps;
            this.CookiesValues = this.GetCookiesValues(context);
        }
    }

    /// <summary>
    /// Get cookies values.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    /// <returns>
    /// Cookies values.
    /// </returns>
    private string GetCookiesValues(HttpContext context)
    {
        try
        {
            if (context.Request.Cookies.IsNotNull()
                && context.Request.Cookies.Count > 0)
            {
                StringBuilder cookieValue = new StringBuilder();
                foreach (var cookie in context.Request.Cookies)
                {
                    cookieValue.Append($"{cookie.Key}:{cookie.Value}");
                }

                return cookieValue.ToString();
            }
        }
        catch 
        {
            return StringValues.FailedToLoad;
        }

        return string.Empty;
    }

    /// <summary>
    /// Get headers values.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    /// <returns>
    /// Headers values.
    /// </returns>
    private string GetHeadersValues(HttpContext context)
    {
        try
        {
            if (context.Request.Headers.Count > 0)
            {
                StringBuilder headersValue = new StringBuilder();
                foreach (var header in context.Request.Headers)
                {
                    headersValue.Append($"{header.Key}:{header.Value}");
                }

                return headersValue.ToString();
            }
        }
        catch
        {
            return StringValues.FailedToLoad;
        }

        return string.Empty;
    }
}
