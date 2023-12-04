using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings.Database;

namespace Roy.Logging.Aspect.Database.Helpers;

/// <summary>
/// Setting manager.
/// </summary>
internal class SettingManager
{
    /// <summary>
    /// Set the database setting.
    /// </summary>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <param name="isExceptionType">
    /// Flag that determinate whether the query need it is for exceptions or for logs.
    /// </param>
    public QuerySetting GetQuerySettings(DatabaseSetting setting, bool isExceptionType)
    {
        QuerySetting query = new QuerySetting(setting, 
            this.GetQuery(isExceptionType));
        this.SetTableName(query.Database, isExceptionType);

        return query;
    }

    /// <summary>
    /// Get the query used to save the issue (exceptions/logs) in the database.
    /// </summary>
    /// <param name="isExceptionType">
    /// Flag that determinate whether the query need it is for exceptions or for logs.
    /// </param>
    /// <returns>
    /// Query used to save the issue (exceptions/logs) in the database.
    /// </returns>
    private string GetQuery(bool isExceptionType)
    {
        if (isExceptionType)
        {
            return Queries.InsertExceptionQuery;
        }

        return Queries.InsertLogQuery;
    }

    /// <summary>
    /// Set the table's name.
    /// </summary>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <param name="isExceptionType">
    /// Flag that determinate whether the query need it is for exceptions or for logs.
    /// </param>
    private void SetTableName(DatabaseSetting setting, bool isExceptionType)
    {
        if (setting.TableName.IsNullOrEmpty())
        {
            if (isExceptionType)
            {
                setting.TableName = SchemaValues.DefaultExceptionTableName;
            }
            else
            {
                setting.TableName = SchemaValues.DefaultLogTableName;
            }
        }
    }
}