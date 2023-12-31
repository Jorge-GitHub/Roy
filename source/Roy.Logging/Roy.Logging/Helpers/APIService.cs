using Avalon.Base.Extension.Collections;
using Roy.Logging.Aspect.API;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Communication;
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
    /// <param name="process">
    /// Message returned by the logging service.
    /// </param>
    public void Post(MessageDetail message, 
        List<APISetting> settings, InternalProcessMessage process)
    {
        foreach (APISetting api in settings)
        {
            try
            {
                if ((!api.LevelsToPost.HasElements() ||
                    api.LevelsToPost.Any(item => item.Equals(message.Level)))
                    && !api.DisablePost)
                {
                    this.Utility.Post(api, message);
                }
            }
            catch (Exception ex)
            {
                process.Errors.Add(ex);
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