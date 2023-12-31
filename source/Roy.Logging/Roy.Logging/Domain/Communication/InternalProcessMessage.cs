using Avalon.Base.Extension.Collections;

namespace Roy.Logging.Domain.Communication;

/// <summary>
/// Message returned by the logging service.
/// </summary>
public class InternalProcessMessage
{
    /// <summary>
    /// Flag that indicates whether the process succeeded.
    /// </summary>
    public bool Succeed => this.Errors.HasElements();
    /// <summary>
    /// Errors throw while saving/logging the object.
    /// </summary>
    public List<Exception>  Errors { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public InternalProcessMessage()
    {
        this.Errors = new List<Exception>();
    }
}