using Roy.Logging.Aspect.Database.Helpers;

namespace Roy.Logging.Domain.Settings.Database;

internal class QuerySetting
{
    public DatabaseSetting Database { get; set; }
    /// <summary>
    // Query to run.
    /// </summary>
    public string Query { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <param name="query">
    /// Query to run.
    /// </param>
    public QuerySetting(DatabaseSetting setting, string query)
    {
        this.InitializeObject(setting, query);
    }

    /// <summary>
    /// Initialize the object.
    /// </summary>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <param name="query">
    /// Query to run.
    /// </param>
    private void InitializeObject(DatabaseSetting setting, string query)
    {
        this.Database = setting;
        this.Query = query;
    }
}
