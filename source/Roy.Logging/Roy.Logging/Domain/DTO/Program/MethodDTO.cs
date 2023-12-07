namespace Roy.Logging.Domain.DTO.Program;

/// <summary>
/// Stack frame making the call to the method data transfer object (DTO).
/// </summary>
public class MethodDTO
{
    /// <summary>
    /// File name containing the code being executed. This
    /// information is normally extracted from the debugging symbols
    /// for the executable.
    /// </summary>
    public string? CallerFileName { get; set; }
    /// <summary>
    /// Method name making the call.
    /// </summary>
    public string? CallerMethodName { get; set; }
    /// <summary>
    /// Line number in the file containing the code being executed.
    /// This information is normally extracted from the debugging symbols
    /// for the executable.
    /// </summary>
    public int CallerLineNumber { get; set; }
    /// <summary>
    /// Parameters.
    /// </summary>
    public List<ParameterDTO>? Parameters { get; set; }
}