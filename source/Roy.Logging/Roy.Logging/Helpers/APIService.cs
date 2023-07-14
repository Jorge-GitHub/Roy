using Avalon.Base.Extension.Collections;
using Roy.Domain.Attributes;
using Roy.Domain.Settings.Web.APIAspect;
using Roy.Logging.Aspect.API;

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