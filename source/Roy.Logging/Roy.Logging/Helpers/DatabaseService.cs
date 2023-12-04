using Roy.Logging.Aspect.Database;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Database;

namespace Roy.Logging.Helpers;

/// <summary>
/// Database service.
/// </summary>
internal class DatabaseService
{
    /// <summary>
    /// Database utility.
    /// </summary>
    private DatabaseUtility Utility {  get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DatabaseService()
    {
        this.InitializeObject();
    }

    /// <summary>
    /// Save the message into the databases.
    /// </summary>
    /// <param name="message">
    /// Messages to save.
    /// </param>
    /// <param name="settings">
    /// List of database's settings.
    /// </param>
    public async void SaveAsync(MessageDetail message,
        List<DatabaseSetting> settings)
    {
        foreach(DatabaseSetting setting in settings)
        {
            // 1 create a record object
            this.Utility.Save(message, setting);
            // // (object is going to be used to limit the string lengths and set the default table name and query type)
            // 2 create script
            // 3 submit query
        }
    }

    /// <summary>
    /// Initialize the object.
    /// </summary>
    private void InitializeObject()
    {
        this.Utility = new DatabaseUtility();
    }
}