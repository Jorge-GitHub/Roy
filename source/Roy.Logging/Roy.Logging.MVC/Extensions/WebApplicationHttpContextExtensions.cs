using Avalon.Base.Extension.Types;
using Microsoft.AspNetCore.Http;
using Roy.Logging.Domain.Program;

namespace Roy.Logging.MVC.Extensions;

/// <summary>
/// Web application HttpContext extensions.
/// </summary>
public static class WebApplicationHttpContextExtensions
{
    /// <summary>
    /// To web application HttpContext object.
    /// </summary>
    /// <param name="context">
    /// HTTP-specific information about an individual HTTP request.
    /// </param>
    /// <returns>
    /// Web application HttpContext object.
    /// </returns>
    public static WebApplicationHttpContext? ToWebApplicationHttpContext(
        this HttpContext context)
    {
        try
        {
            if (context.Request.IsNotNull())
            {
                WebApplicationHttpContext webAppContext = new WebApplicationHttpContext();

                if (context.Request.QueryString.IsNotNull())
                {
                    webAppContext.CurrentURL = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path.ToString()}{context.Request.QueryString.ToString()}";
                    webAppContext.CurrentURLParameters = context.Request.QueryString.ToString();
                }
                if (context.Request.Headers.IsNotNull())
                {
                    webAppContext.UserLanguagePreferences = context.Request.Headers["Accept-Language"]
                        .ToSafeString().Split(";")
                        .FirstOrDefault()?.Split(",").FirstOrDefault();
                    webAppContext.PreviousURL = context.Request.Headers.Referer.ToString();
                    webAppContext.HeadersValues = WebApplicationHttpContextExtensions
                        .GetValues(context.Request.Headers);
                }
                if (context.Connection.IsNotNull())
                {
                    webAppContext.UserHostIP = context.Connection.RemoteIpAddress.ToSafeString();
                }
                if (context.Request.Host.IsNotNull())
                {
                    webAppContext.UserDomainName = context.Request.Host.Value;
                }
                webAppContext.IsSecureConnection = context.Request.IsHttps;
                webAppContext.CookiesValues = WebApplicationHttpContextExtensions
                    .GetValues(context.Request.Cookies);

                return webAppContext;
            }           
        }
        catch
        {
            return new WebApplicationHttpContext() { FailedToLoad = true };
        }

        return null;
    }

    /// <summary>
    /// Get values from key value pair list.
    /// </summary>
    /// <param name="values">
    /// values to read.
    /// </param>
    /// <returns>
    /// key value pair list value.
    /// </returns>
    public static List<string> GetValues(this IEnumerable<KeyValuePair<string, string>> values)
    {
        List<string> stringValues = new List<string>();
        try
        {
            if (values.IsNotNull() && values.Count() > 0)
            {
                foreach (KeyValuePair<string, string> item in values)
                {
                    stringValues.Add($"{item.Key}:{item.Value}");
                }
            }
        }
        catch
        {
            stringValues.Add(Logging.Domain.Contants.StringValues.FailedToLoad);
        }

        return stringValues;
    }

    /// <summary>
    /// Get values from key value pair list.
    /// </summary>
    /// <param name="values">
    /// values to read.
    /// </param>
    /// <returns>
    /// key value pair list value.
    /// </returns>
    public static List<string> GetValues(this IEnumerable<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>> values)
    {
        List<string> stringValues = new List<string>();
        try
        {
            if (values.IsNotNull() && values.Count() > 0)
            {
                foreach (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> item in values)
                {
                    stringValues.Add($"{item.Key}:{item.Value}");
                }
            }
        }
        catch
        {
            stringValues.Add(Logging.Domain.Contants.StringValues.FailedToLoad);
        }

        return stringValues;
    }

}