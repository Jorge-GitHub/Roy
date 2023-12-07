using Roy.Logging.Aspect.Database;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Communication;
using Roy.Logging.Domain.Settings.Database;
using Roy.Logging.Extensions;

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
    /// <param name="process">
    /// Message returned by the logging service.
    /// </param>
    public void Save(MessageDetail message,
        List<DatabaseSetting> settings, ProcessMessage process)
    {
        foreach(DatabaseSetting setting in settings)
        {
            try
            {
                setting.SetDefaultValues(message.Level, message.IsExceptionType());
                this.Utility.Save(message, setting);
            }
            catch (Exception ex)
            {
                process.Errors.Add(ex);
            }
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