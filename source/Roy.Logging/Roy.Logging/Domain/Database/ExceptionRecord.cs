using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;

namespace Roy.Logging.Domain.Database;

/// <summary>
/// Exception record to save in the database.
/// </summary>
internal class ExceptionRecord : Record
{
    /// <summary>
    /// Exception's message.
    /// </summary>
    public string ExceptionMessage { get; set; }
    /// <summary>
    /// Exception's stack trace.
    /// </summary>
    public string ExceptionStackTrace { get; set; }
    /// <summary>
    /// Exception in JSON format.
    /// </summary>
    public string ExceptionJSON { get; set; }
    /// <summary>
    /// Exception source.
    /// </summary>
    public string Source { get; set; }
    /// <summary>
    /// Exception help link.
    /// </summary>
    public string HelpLink { get; set; }
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
    /// Exception message's details.
    /// </param>
    private void InitializeObject(ExceptionDetail message)
    {
        this.ExceptionJSON = message.ExceptionTrace.ToJSON();
        this.ExceptionMessage = message.ExceptionMessage;
        this.ExceptionStackTrace = message.StackTrace;
        if (message.ExceptionTrace.IsNotNull())
        {
            this.Source = message.ExceptionTrace.Source;
            this.HelpLink = message.ExceptionTrace.HelpLink.LimitLength(250);
        }
    }
}