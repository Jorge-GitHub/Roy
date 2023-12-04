using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Settings.Database;
using Roy.Logging.Extensions;
using System.Text;

namespace Roy.Logging.Aspect.Database.Helpers;

/// <summary>
/// Query builder helper.
/// </summary>
internal class QueryBuilder
{
    /// <summary>
    /// Schema helper.
    /// </summary>
    private SettingManager SchemaHelper {  get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public QueryBuilder()
    {
        this.SchemaHelper = new SettingManager();
    }

    /// <summary>
    /// Creates the query to run to insert the message in the database.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="message">
    /// Message detail to use for building the query.
    /// </param>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <returns>
    /// Query.
    /// </returns>
    public StringBuilder Create(MessageDetail message, QuerySetting querySetting)
    {
        bool isExceptionType = message.IsExceptionType();
        StringBuilder query = new StringBuilder(querySetting.Query);
        this.PopulateMessageDetails(query, message);
        if (isExceptionType)
        {
            this.PopulateExceptionDetails(query, (ExceptionDetail)message);
        }
        else
        {
            this.PopulateLogDetails(query, (LogDetail)message);
        }

        return query;
    }

    /// <summary>
    /// Populate the message details in the query.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="message">
    /// Object used to populate the query.
    /// </param>
    private void PopulateMessageDetails(StringBuilder query, MessageDetail message)
    {
        query.Replace(Tags.Id, message.Id);
    }

    /// <summary>
    /// Populate the exception's details in the query.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="message">
    /// Object used to populate the query.
    /// </param>
    private void PopulateExceptionDetails(StringBuilder query, MessageDetail message)
    {
    }

    /// <summary>
    /// Populate the log's details in the query.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="message">
    /// Object used to populate the query.
    /// </param>
    private void PopulateLogDetails(StringBuilder query, MessageDetail message)
    {
    } 
}