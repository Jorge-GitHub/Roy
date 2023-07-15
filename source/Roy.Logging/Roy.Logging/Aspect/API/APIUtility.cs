using Avalon.Base.Extension.Types;
using Roy.Domain.Attributes;
using Roy.Domain.Settings.Web.APIAspect;
using Roy.Logging.Extensions;
using System.Net.Http.Json;

namespace Roy.Logging.Aspect.API;

/// <summary>
/// API Service.
/// </summary>
internal class APIUtility
{
    /// <summary>
    /// Post the message to the API.
    /// </summary>
    /// <param name="settings">
    /// API settings.
    /// </param>
    /// <param name="message">
    /// Message to post.
    /// </param> 
    public void Post(APISetting setting, MessageDetail message)
    {
        if (setting.URL.IsNotNullOrEmpty())
        {
            HttpClient client = null;
            try
            {
                client = new HttpClient();
                if(message.IsExceptionType())
                {
                    client.PostAsJsonAsync(setting.URL, message as ExceptionDetail);
                }
                else
                {
                    client.PostAsJsonAsync(setting.URL, message as LogDetail);
                }
            } 
            catch { } // We let the system keep posting messages to the other APIs.
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
        }
    }
}