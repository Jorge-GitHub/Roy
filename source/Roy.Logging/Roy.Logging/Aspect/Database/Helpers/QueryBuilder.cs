using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings.Database;
using System.Text;

namespace Roy.Logging.Aspect.Database.Helpers;

/// <summary>
/// Query builder helper.
/// </summary>
internal class QueryBuilder
{
    /// <summary>
    /// Creates the query to run to insert the message in the database.
    /// </summary>
    /// <param name="message">
    /// Message detail to use for building the query.
    /// </param>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <returns>
    /// Query.
    /// </returns>
    public StringBuilder Create(MessageDetail message, DatabaseSetting setting)
    {
        StringBuilder query = new StringBuilder(setting.Query);
        this.PopulateDatabaseValues(query, setting);

        return query;
    }

    /// <summary>
    /// Populates the database values.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    private void PopulateDatabaseValues(StringBuilder query, DatabaseSetting setting)
    {
        query.Replace(DatabaseTag.DatabaseName, setting.DatabaseName);
        query.Replace(DatabaseTag.TableName, setting.TableName);
    }
}