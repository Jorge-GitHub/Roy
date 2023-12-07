namespace Roy.Logging.Domain.DTO.Program;

/// <summary>
/// Parameter information data transfer object (DTO).
/// </summary>
public class ParameterDTO
{
    /// <summary>
    /// Parameter's name.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Parameter's value.
    /// </summary>
    public string? Value { get; set; }
}