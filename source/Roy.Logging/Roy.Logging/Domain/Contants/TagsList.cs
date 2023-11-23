namespace Roy.Logging.Domain.Contants;

/// <summary>
/// Tag lists.
/// </summary>
public static class TagsList
{
    /// <summary>
    /// Exception trace tags.
    /// </summary>
    public static readonly string[] ExceptionTraceTags = { Tags.Source, Tags.HelpLink };
    /// <summary>
    /// Method tags.
    /// </summary>
    public static readonly string[] MethodTags =
    {
        Tags.MethodCallerFileName, Tags.MethodCallerMethodName,
        Tags.MethodCallerLineNumber, Tags.MethodParametersJSON
    };
    /// <summary>
    /// Machine tags.
    /// </summary>
    public static readonly string[] MachineTags =
    {
        Tags.MachineCLRVersion, Tags.MachineDomainName,
        Tags.MachineName, Tags.MachineOperativeSystem,
        Tags.MachineOperativeSystemVersion, Tags.MachineUserAccountName
    };
    /// <summary>
    /// Application tags.
    /// </summary>
    public static readonly string[] ApplicationTags =
    {
        Tags.AssemblyLocation
    };
    /// <summary>
    /// Application tags.
    /// </summary>
    public static readonly string[] WebApplicationTags =
    {
    };
}