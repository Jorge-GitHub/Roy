using Avalon.Base.Extension.Collections;

namespace Roy.Logging.Domain.DTO.Communication;

/// <summary>
/// Data transfer object (DTO) returned by the logging service.
/// </summary>
public class ProcessMessageDTO
{
    /// <summary>
    /// Flag that indicates whether the process succeeded.
    /// </summary>
    public bool Succeed => this.Errors.HasElements();
    /// <summary>
    /// Errors throw while saving/logging the object.
    /// </summary>
    public List<Exception> Errors { get; set; }
}