using Avalon.Base.Extension.Collections;
using Roy.Logging.Aspect.API;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Web.APIAspect;

namespace Roy.Logging.Helpers;

/// <summary>
/// API service.
/// </summary>
internal class APIService
{
    private APIUtility Utility { get; set; }

    /// <summary>
    /// Post the message to the APIs.
    /// </summary>
    /// <param name="message">
    /// Message to post.
    /// </param>
    /// <param name="settings">
    /// API settings.
    /// </param>
    public async void PostAsync(MessageDetail message, List<APISetting> settings)
    {
        foreach (APISetting api in settings)
        {
            if ((!api.LevelsToPost.HasElements() ||
                api.LevelsToPost.Any(item => item.Equals(message.Level)))
                && !api.DisablePost)
            {
                this.Utility.Post(api, message);
            }
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public APIService()
    {
        this.Utility = new APIUtility();
    }
}