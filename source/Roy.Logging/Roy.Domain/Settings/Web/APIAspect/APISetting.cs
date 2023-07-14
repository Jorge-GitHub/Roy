using Roy.Domain.Contants;

namespace Roy.Domain.Settings.Web.APIAspect;

/// <summary>
/// API settings.
/// </summary>
public class APISetting
{
    /// <summary>
    /// Issues levels to post. If null or empty, it will post all the issues.
    /// </summary>
    public List<Level> LevelsToPost { get; set; }
    /// <summary>
    /// Flag that determinate whether to disable or not the posting of the messages.
    /// </summary>
    public bool DisablePost { get; set; }
}
