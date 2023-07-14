using Avalon.Base.Extension.Collections;
using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using Roy.Domain.Settings.Web.APIAspect;
using Roy.Domain.Settings.Web.EmailAspect;
using Roy.Logging.Aspect.API;

namespace Roy.Logging.Helpers;

/// <summary>
/// API service.
/// </summary>
internal class APIService
{
    private APIUtility Utility { get; set; }


    public async void PostAsync(MessageDetail bodyDetail, List<APISetting> settings)
    {
        foreach (APISetting setting in settings)
        {
            if ((!setting.LevelsToPost.HasElements() ||
                setting.LevelsToPost.Any(item => item.Equals(bodyDetail.Level)))
                && !setting.DisablePost)
            {
                this.Utility.Post();
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