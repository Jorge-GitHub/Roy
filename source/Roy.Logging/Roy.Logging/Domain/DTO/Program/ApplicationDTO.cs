namespace Roy.Logging.Domain.DTO.Program;

/// <summary>
/// Application details data transfer object (DTO).
/// </summary>
public class ApplicationDTO
{
    /// <summary>
    /// Flag that indicates whether the current 
    /// application is in debug mode or not.
    /// </summary>
    public bool IsDebuggingEnabled { get; set; }
    /// <summary>
    /// Physical file system location of the 
    /// current executing server application.
    /// </summary>
    public string? PhysicalApplicationPath { get; set; }
    /// <summary>
    /// Assembly that the current code is running from.
    /// </summary>
    public string? AssemblyLocation { get; set; }
    /// <summary>   
    /// Application friendly name.
    /// </summary>
    public string? FriendlyName { get; set; }
    /// <summary>
    /// Flag that determinate whether the application is executing in full trust or not.
    /// </summary>
    public bool IsFullyTrusted { get; set; }
    /// <summary>
    /// Network domain name associated with the current user.
    /// </summary>
    public string? UserDomainName { get; set; }
    /// <summary>
    /// User name of the current thread.
    /// </summary>
    public string? UserName { get; set; }
}