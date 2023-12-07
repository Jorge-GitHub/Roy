using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.DTO.Program;
using Roy.Logging.Domain.Program;

namespace Roy.Logging.Domain.DTO;

/// <summary>
/// Message's detail data transfer object (DTO).
/// </summary>
public class MessageDTO
{
    /// <summary>
    /// Date time.
    /// </summary>
    public DateTimeOffset? Date { get; set; }
    /// <summary>
    /// Exception level.
    /// </summary>
    public string? Level { get; set; }
    /// <summary>
    /// Id.
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Log message.
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Machine/Server information.
    /// </summary>
    public MachineDTO? MachineInformation { get; set; }
    /// <summary>
    /// Application's information.
    /// </summary>
    public ApplicationDTO? ApplicationInformation { get; set; }
    /// <summary>
    /// Web application's information.
    /// </summary>
    public WebApplicationDTO? WebApplicationInformation { get; set; }
    /// <summary>
    /// Stack frame making the call to the method.
    /// </summary>
    public MethodDTO? StackFrame { get; set; }
    /// <summary>
    /// list of parameters.
    /// </summary>
    public object[]? CustomListOfParameters { get; set; }
}
