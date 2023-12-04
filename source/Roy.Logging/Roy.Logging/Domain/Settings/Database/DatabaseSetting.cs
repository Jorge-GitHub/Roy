using Roy.Logging.Domain.Contants;
using System.Globalization;

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
    public List<Level> LevelsToSave { get; set; }
    /// <summary>
    /// Language. Language to use for the email format. 
    /// Currently Roy supports English, Spanish, French, and German. 
    /// The default value is English.
    /// </summary>
    public Language Language { get; set; }
    /// <summary>
    /// Culture.
    /// </summary>
    public CultureInfo Culture { get; set; }
    /// <summary>
    // Query to run.
    /// </summary>
    public string Query { get; set; }
}