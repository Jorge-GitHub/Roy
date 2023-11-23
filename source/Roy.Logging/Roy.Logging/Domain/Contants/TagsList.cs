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
        Tags.AssemblyLocation, Tags.ApplicationIsDebuggingEnabled,
        Tags.ApplicationPhysicalApplicationPath, Tags.ApplicationFriendlyName,
        Tags.ApplicationIsFullyTrusted, Tags.ApplicationUserDomainName,
        Tags.ApplicationUserName
    };
    /// <summary>
    /// Web application tags.
    /// </summary>
    public static readonly string[] WebApplicationTags =
    {
        Tags.WebApplicationCurrentURL,
        Tags.WebApplicationCurrentURLParameters,
        Tags.WebApplicationPreviousURL,
        Tags.WebApplicationUserHostIP,
        Tags.WebApplicationIsSecureConnection,
        Tags.WebApplicationUserDomainName,
        Tags.WebApplicationCookiesValues,
        Tags.WebApplicationHeadersValues,
        Tags.WebApplicationUserLanguagePreferences,
    };
}