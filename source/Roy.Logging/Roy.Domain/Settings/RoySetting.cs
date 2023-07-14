using Roy.Domain.Settings.Attributes;

namespace Roy.Domain.Settings;

/// <summary>
/// Log settings.
/// </summary>
public class RoySetting
{
    /// <summary>
    /// Exception settings.
    /// </summary>
    public IssueSetting Exception { get; set; }
    /// <summary>
    /// Exception settings.
    /// </summary>
    public IssueSetting Log { get; set; }
    /// <summary>
    /// Constructor.
    /// </summary>
    public RoySetting()
    {
        this.LoadObject();
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    private void LoadObject()
    {
        this.Exception = new IssueSetting();
        this.Exception.DefaultFolderName = "exceptions";
        this.Exception.SaveLogOnFile = true;
        this.Log = new IssueSetting();
        this.Log.DefaultFolderName = "logs";
        this.Log.SaveLogOnFile = true;
    }
}