﻿using Roy.Domain.Settings.Attributes;

namespace Roy.Domain.Settings;

/// <summary>
/// Log settings.
/// </summary>
public class RoySetting
{
    /// <summary>
    /// Exception settings.
    /// </summary>
    public Setting Exception { get; set; }
    /// <summary>
    /// Exception settings.
    /// </summary>
    public Setting Log { get; set; }
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
        this.Exception = new Setting();
        this.Exception.DefaultFolderName = "exceptions";
        this.Exception.SaveLogOnFile = true;
        this.Log = new Setting();
        this.Log.DefaultFolderName = "logs";
        this.Log.SaveLogOnFile = true;
    }
}