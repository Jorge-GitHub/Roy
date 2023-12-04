using Roy.Logging.Domain.Attributes;

namespace Roy.Logging.Domain.Database;

/// <summary>
/// Exception record to save in the database.
/// </summary>
internal class ExceptionRecord : Record
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    public ExceptionRecord(ExceptionDetail message) : base(message)
    {
        this.InitializeObject(message);
    }

    /// <summary>
    /// Initialize the object.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    private void InitializeObject(ExceptionDetail message)
    {
    }
}