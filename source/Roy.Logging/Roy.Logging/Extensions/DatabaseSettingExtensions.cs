using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings.Database;

namespace Roy.Logging.Extensions;

/// <summary>
/// Database extensions.
/// </summary>
internal static class DatabaseSettingExtensions
{
    /// <summary>
    /// Set the default database settings values.
    /// </summary>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <param name="level">
    /// Issue level.
    /// </param>
    /// <param name="isAnException">
    /// Flag that determinate whether the issue is an exception or a log.
    /// </param>
    public static void SetDefaultValues(this DatabaseSetting setting,
        Level level, bool isAnException)
    {
        setting.Culture = setting.Language.ToCultureInfo();
        setting.SetTableName(isAnException);
        setting.SetQuery(isAnException);
    }

    /// <summary>
    /// Set the table's name.
    /// </summary>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <param name="isAnException">
    /// Flag that determinate whether the issue is an exception or a log.
    /// </param>
    public static void SetTableName(this DatabaseSetting setting, bool isAnException)
    {
        if (setting.TableName.IsNullOrEmpty())
        {
            if (isAnException)
            {
                setting.TableName = DatabaseTag.DefaultExceptionTableName;
            }
            else
            {
                setting.TableName = DatabaseTag.DefaultLogTableName;
            }
        }
    }

    /// <summary>
    /// Set the query used to save the issue (exceptions/logs) in the database.
    /// </summary>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <param name="isExceptionType">
    /// Flag that determinate whether the query need it is for exceptions or for logs.
    /// </param>
    private static void SetQuery(this DatabaseSetting setting, 
        bool isExceptionType)
    {
        if (isExceptionType)
        {
            setting.Query = Queries.InsertExceptionQuery;
        }

        setting.Query = Queries.InsertLogQuery;
    }
}