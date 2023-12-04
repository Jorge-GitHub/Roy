using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;

namespace Roy.Logging.Domain.Database;

/// <summary>
/// Log record to save in the database.
/// </summary>
internal class LogRecord : Record
{
    /// <summary>
    /// Log value in JSON format.
    /// </summary>
    public string LogValueInJSONFormat { get; set; }
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    public LogRecord(LogDetail message) : base(message)
    {
        this.InitializeObject(message);
    }

    /// <summary>
    /// Initialize the object.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    private void InitializeObject(LogDetail message)
    {
        this.LogValueInJSONFormat = message.LogValue.ToJSON();
    }
}