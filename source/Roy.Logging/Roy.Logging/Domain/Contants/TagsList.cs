namespace Roy.Logging.Domain.Contants;

/// <summary>
/// Tag lists.
/// </summary>
public static class TagsList
{
    /// <summary>
    /// Exception trace tags.
    /// </summary>
    public static readonly string[] ExceptionTraceTags = { Tag.Source, Tag.HelpLink };
    /// <summary>
    /// Method tags.
    /// </summary>
    public static readonly string[] MethodTags =
    {
        Tag.MethodCallerFileName, Tag.MethodCallerMethodName,
        Tag.MethodCallerLineNumber, Tag.MethodParametersJSON
    };
    /// <summary>
    /// Machine tags.
    /// </summary>
    public static readonly string[] MachineTags =
    {
        Tag.MachineCLRVersion, Tag.MachineDomainName,
        Tag.MachineName, Tag.MachineOperativeSystem,
        Tag.MachineOperativeSystemVersion, Tag.MachineUserAccountName
    };
    /// <summary>
    /// Application tags.
    /// </summary>
    public static readonly string[] ApplicationTags =
    {
        Tag.AssemblyLocation, Tag.ApplicationIsDebuggingEnabled,
        Tag.ApplicationPhysicalApplicationPath, Tag.ApplicationFriendlyName,
        Tag.ApplicationIsFullyTrusted, Tag.ApplicationUserDomainName,
        Tag.ApplicationUserName
    };
    /// <summary>
    /// Web application tags.
    /// </summary>
    public static readonly string[] WebApplicationTags =
    {
        Tag.WebApplicationCurrentURL,
        Tag.WebApplicationCurrentURLParameters,
        Tag.WebApplicationPreviousURL,
        Tag.WebApplicationUserHostIP,
        Tag.WebApplicationIsSecureConnection,
        Tag.WebApplicationUserDomainName,
        Tag.WebApplicationCookiesValues,
        Tag.WebApplicationHeadersValues,
        Tag.WebApplicationUserLanguagePreferences,
    };
}