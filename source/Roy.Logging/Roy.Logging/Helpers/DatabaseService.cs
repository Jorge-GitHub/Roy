using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Database;

namespace Roy.Logging.Helpers;

/// <summary>
/// Database service.
/// </summary>
public class DatabaseService
{
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
        foreach(DatabaseSetting database in settings)
        {

        }
    }
}