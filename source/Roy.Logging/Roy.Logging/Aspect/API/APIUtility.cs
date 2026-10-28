using Avalon.Base.Extensions.Types;
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
    /// <param name="setting">
    /// API settings.
    /// </param>
    /// <param name="message">
    /// Message to post.
    /// </param> 
    public async Task PostAsync(APISetting setting, MessageDetail message)
    {
        if (setting.URL.IsNotNullOrEmpty())
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response;
                if (message.IsExceptionType())
                {
                    ExceptionDTO exception = (message as ExceptionDetail).ToDTO();
                    response = await client.PostAsJsonAsync(setting.URL, exception)
                        .ConfigureAwait(false);
                }
                else
                {
                    LogDTO log = (message as LogDetail).ToDTO();
                    response = await client.PostAsJsonAsync(setting.URL, log)
                        .ConfigureAwait(false);
                }
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
