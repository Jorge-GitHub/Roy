using Roy.Logging.Aspect.Database.Helpers;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Database;
using System.Text;

namespace Roy.Logging.Aspect.Database;

/// <summary>
/// Database utility.
/// </summary>
internal class DatabaseUtility
{
    /// <summary>
    /// Query builder helper.
    /// </summary>
    private QueryBuilder Builder {  get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DatabaseUtility()
    {
        this.Builder = new QueryBuilder();
    }

    /// <summary>
    /// Save the message detail in the database.
    /// </summary>
    /// <param name="message">
    /// Message to save.
    /// </param>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    public void Save(MessageDetail message, DatabaseSetting setting)
    {
        // Create query
        StringBuilder query = this.Builder.Create(message);

        // Save on database.
    }
}