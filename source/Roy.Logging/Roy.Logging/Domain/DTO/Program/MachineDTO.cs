namespace Roy.Logging.Domain.DTO.Program;

/// <summary>
/// Machine environment data transfer object (DTO).
/// </summary>
public class MachineDTO
{
    /// <summary>
    /// .NET CLR Version.
    /// </summary>
    public string? CLRVersion { get; set; }
    /// <summary>
    /// Domain name.
    /// </summary>
    public string? DomainName { get; set; }
    /// <summary>
    /// Machine's name.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Operative system version.
    /// </summary>
    public string? OperativeSystemVersion { get; set; }
    /// <summary>
    /// User account name.
    /// </summary>
    public string? UserAccountName { get; set; }
    /// <summary>
    /// Operative system.
    /// </summary>
    public string? OperativeSystem { get; set; }
}