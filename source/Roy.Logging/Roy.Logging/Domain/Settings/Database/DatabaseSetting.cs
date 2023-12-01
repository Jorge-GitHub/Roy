using Roy.Logging.Domain.Contants;

namespace Roy.Logging.Domain.Settings.Database;

/// <summary>
/// Database settings.
/// </summary>
public class DatabaseSetting
{
    /// <summary>
    /// Database's name.
    /// </summary>
    public string DatabaseName { get; set; }
    /// <summary>
    /// String connection.
    /// </summary>
    public string StringConnection { get; set; }
    /// <summary>
    /// Table's name.
    /// </summary>
    public string TableName { get; set; }
    /// <summary>
    /// Flag that determinate whether to disable or 
    /// not the logging of the issue on the database.
    /// </summary>
    public bool Disable { get; set; }
    /// <summary>
    /// Issues levels to save. If null or empty, it will save all the issues.
    /// </summary>
    public List<Level> LevelsToSave{ get; set; }
    /// <summary>
    /// Default table's name.
    /// </summary>
    private string DefaultTableName { get; set; }

    /// <summary>
    /// Sets the default table's name.
    /// </summary>
    /// <param name="defaultTableName">
    /// Default table's name.
    /// </param>
    public void SetDefaultTableName(string defaultTableName)
    {
        this.DefaultTableName = defaultTableName;
    }
}