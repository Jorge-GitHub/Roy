using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.DTO;
using Roy.Logging.Domain.Settings.Web.APIAspect;
using Roy.Logging.Extensions;
using Roy.Logging.Extensions.DTO;
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
                    ExceptionDTO exception = (message as ExceptionDetail).ToDTO();
                    client.PostAsJsonAsync(setting.URL, exception);
                }
                else
                {
                    LogDTO log = (message as LogDetail).ToDTO();
                    client.PostAsJsonAsync(setting.URL, log);
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