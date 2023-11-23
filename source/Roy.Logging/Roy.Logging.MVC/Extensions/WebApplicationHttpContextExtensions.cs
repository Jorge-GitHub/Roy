using Avalon.Base.Extension.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using System.Text;

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
    public static WebApplicationHttpContext ToWebApplicationHttpContext(
        this HttpContext context)
    {
        WebApplicationHttpContext webAppContext = new WebApplicationHttpContext();
        try
        {
            if (context.Request.IsNotNull())
            {
                if (context.Request.QueryString.IsNotNull())
                {
                    webAppContext.CurrentURL = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path.ToString()}{context.Request.QueryString.ToString()}";
                    webAppContext.CurentURLParameters = context.Request.QueryString.ToString();
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
                    webAppContext.DomainName = context.Request.Host.Value;
                }
                webAppContext.IsSecureConnection = context.Request.IsHttps;
                webAppContext.CookiesValues = WebApplicationHttpContextExtensions
                    .GetValues(context.Request.Cookies);
            }
        }
        catch
        {
            webAppContext.FailedToLoad = true;
        }

        return webAppContext;
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
    public static string GetValues(this IEnumerable<KeyValuePair<string, string>> values)
    {
        try
        {
            if (values.IsNotNull() && values.Count() > 0)
            {
                StringBuilder stringValue = new StringBuilder();
                foreach (KeyValuePair<string, string> item in values)
                {
                    stringValue.Append($"{item.Key}:{item.Value}");
                }

                return stringValue.ToString();
            }
        }
        catch
        {
            return StringValue.FailedToLoad;
        }

        return string.Empty;
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
    public static string GetValues(this IEnumerable<KeyValuePair<string, StringValues>> values)
    {
        try
        {
            if (values.IsNotNull() && values.Count() > 0)
            {
                StringBuilder stringValue = new StringBuilder();
                foreach (KeyValuePair<string, StringValues> item in values)
                {
                    stringValue.Append($"{item.Key}:{item.Value}");
                }

                return stringValue.ToString();
            }
        }
        catch
        {
            return StringValue.FailedToLoad;
        }

        return string.Empty;
    }

}