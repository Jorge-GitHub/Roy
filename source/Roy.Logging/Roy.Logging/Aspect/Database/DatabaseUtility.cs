using Microsoft.Data.SqlClient;
using Roy.Logging.Aspect.Database.Helpers;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Database;
using Roy.Logging.Extensions;
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
    /// Database logic.
    /// </summary>
    private DatabaseLogic Database {  get; set; }
    /// <summary>
    /// Parameters builder logic.
    /// </summary>
    private ParametersBuilder Parameters{ get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DatabaseUtility()
    {
        this.InitializeObject();
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
        this.Database.ExecuteQuery(setting.StringConnection,
            this.Builder.Create(message, setting).ToString(), 
            this.Parameters.GetParameters(message));
    }

    

    /// <summary>
    /// Initialize the object.
    /// </summary>
    private void InitializeObject()
    {
        this.Builder = new QueryBuilder();
        this.Database = new DatabaseLogic();
        this.Parameters = new ParametersBuilder();
    }
}