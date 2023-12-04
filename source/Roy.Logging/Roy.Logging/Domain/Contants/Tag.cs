namespace Roy.Logging.Domain.Contants;

/// <summary>
/// Tags.
/// </summary>
internal struct Tag
{
    /// <summary>
    /// Id tag for replacing Id on the HTML template.
    /// </summary>
    public const string Id = "*|ID|*";
    /// <summary>
    /// Id tag for replacing date on the HTML template.
    /// </summary>
    public const string Date = "*|Date|*";
    /// <summary>
    /// Id tag for replacing issue's level on the HTML template.
    /// </summary>
    public const string Level = "*|Level|*";
    /// <summary>
    /// Id tag for replacing exception message on the HTML template.
    /// </summary>
    public const string ExceptionMessage = "*|ExceptionMessage|*";
    /// <summary>
    /// Id tag for replacing custom message on the HTML template.
    /// </summary>
    public const string Message = "*|Message|*";
    /// <summary>
    /// Id tag for replacing the custom list of parameters on the HTML template.
    /// </summary>
    public const string CustomListOfParametersJSON = "*|CustomListOfParametersJSON|*";
    /// <summary>
    /// Id tag for replacing source on the HTML template.
    /// </summary>
    public const string Source = "*|Source|*";
    /// <summary>
    /// Id tag for replacing stack trace on the HTML template.
    /// </summary>
    public const string StackTrace = "*|StackTrace|*";
    /// <summary>
    /// Id tag for replacing help link on the HTML template.
    /// </summary>
    public const string HelpLink = "*|HelpLink|*";
    /// <summary>
    /// Id tag for replacing assembly location on the HTML template.
    /// </summary>
    public const string AssemblyLocation = "*|AssemblyLocation|*";
    /// <summary>
    /// Id tag for replacing stack frame in JSON format on the HTML template.
    /// </summary>
    public const string StackFrameJSON = "*|StackFrameJSON|*";
    /// <summary>
    /// Id tag for replacing exception in JSON format on the HTML template.
    /// </summary>
    public const string ExceptionJSON = "*|ExceptionJSON|*";
    /// <summary>
    /// Id tag for replacing the issue in JSON format on the HTML template.
    /// </summary>
    public const string IssueJSON = "*|IssueJSON|*";
    /// <summary>
    /// Id tag for replacing machine domain name on the HTML template.
    /// </summary>
    public const string MachineDomainName = "*|Machine_DomainName|*";
    /// <summary>
    /// Id tag for replacing machine CLR version on the HTML template.
    /// </summary>
    public const string MachineCLRVersion = "*|Machine_CLRVersion|*";
    /// <summary>
    /// Id tag for replacing machine's name on the HTML template.
    /// </summary>
    public const string MachineName = "*|Machine_Name|*";
    /// <summary>
    /// Id tag for replacing machine operative system on the HTML template.
    /// </summary>
    public const string MachineOperativeSystem = "*|Machine_OperativeSystem|*";
    /// <summary>
    /// Id tag for replacing machine operative system version on the HTML template.
    /// </summary>
    public const string MachineOperativeSystemVersion = "*|Machine_OperativeSystemVersion|*";
    /// <summary>
    /// Id tag for replacing machine user account's name on the HTML template.
    /// </summary>
    public const string MachineUserAccountName = "*|Machine_UserAccountName|*";
    /// <summary>
    /// Id tag for replacing current year on the HTML template.
    /// </summary>
    public const string CurrentYear = "*|CurrentYear|*";
    /// <summary>
    /// Id tag for replacing the issue in JSON format on the HTML template.
    /// </summary>
    public const string LogValueJSON = "*|LogValueJSON|*";
    /// <summary>
    /// Id tag for replacing method caller file name on the HTML template.
    /// </summary>
    public const string MethodCallerFileName = "*|Method_CallerFileName|*";
    /// <summary>
    /// Id tag for replacing method caller name on the HTML template.
    /// </summary>
    public const string MethodCallerMethodName = "*|Method_CallerMethodName|*";
    /// <summary>
    /// Id tag for replacing method caller line number on the HTML template.
    /// </summary>
    public const string MethodCallerLineNumber = "*|Method_CallerLineNumber|*";
    /// <summary>
    /// Id tag for replacing method parameters in JSON format on the HTML template.
    /// </summary>
    public const string MethodParametersJSON = "*|Method_Parameters|*";
    /// <summary>
    /// Id tag for replacing the application is debugging enabled value on the HTML template.
    /// </summary>
    public const string ApplicationIsDebuggingEnabled = "*|Application_IsDebuggingEnabled|*";
    /// <summary>
    /// Id tag for replacing the application physical path value on the HTML template.
    /// </summary>
    public const string ApplicationPhysicalApplicationPath = "*|Application_PhysicalApplicationPath|*";
    /// <summary>
    /// Id tag for replacing the application friendly name value on the HTML template.
    /// </summary>
    public const string ApplicationFriendlyName = "*|Application_FriendlyName|*";
    /// <summary>
    /// Id tag for replacing the application is fully trusted value on the HTML template.
    /// </summary>
    public const string ApplicationIsFullyTrusted = "*|Application_IsFullyTrusted|*";
    /// <summary>
    /// Id tag for replacing the application user domain name value on the HTML template.
    /// </summary>
    public const string ApplicationUserDomainName = "*|Application_UserDomainName|*";
    /// <summary>
    /// Id tag for replacing the application user name value on the HTML template.
    /// </summary>
    public const string ApplicationUserName = "*|Application_UserName|*";
    /// <summary>
    /// Id tag for replacing the web application current url value on the HTML template.
    /// </summary>
    public const string WebApplicationCurrentURL = "*|WebApplication_CurrentURL|*";
    /// <summary>
    /// Id tag for replacing the web application current url parameters value on the HTML template.
    /// </summary>
    public const string WebApplicationCurrentURLParameters = "*|WebApplication_CurrentURLParameters|*";
    /// <summary>
    /// Id tag for replacing the web application current previous url value on the HTML template.
    /// </summary>
    public const string WebApplicationPreviousURL = "*|WebApplication_PreviousURL|*";
    /// <summary>
    /// Id tag for replacing the web application user IP on the HTML template.
    /// </summary>
    public const string WebApplicationUserHostIP = "*|WebApplication_UserHostIP|*";
    /// <summary>
    /// Id tag for replacing the web application flag that determinate whether the connection is secure or not value on the HTML template.
    /// </summary>
    public const string WebApplicationIsSecureConnection = "*|WebApplication_IsSecureConnection|*";
    /// <summary>
    /// Id tag for replacing the web application network domain name associated with the current user value on the HTML template.
    /// </summary>
    public const string WebApplicationUserDomainName = "*|WebApplication_UserDomainName|*";
    /// <summary>
    /// Id tag for replacing the web application cookies value on the HTML template.
    /// </summary>
    public const string WebApplicationCookiesValues = "*|WebApplication_CookiesValues|*";
    /// <summary>
    /// Id tag for replacing the web application headers value on the HTML template.
    /// </summary>
    public const string WebApplicationHeadersValues = "*|WebApplication_HeadersValues|*";
    /// <summary>
    /// Id tag for replacing the web application user preference language value on the HTML template.
    /// </summary>
    public const string WebApplicationUserLanguagePreferences = "*|WebApplication_UserLanguagePreferences|*";
    /// <summary>
    /// Id tag that use to know where the method information sections starts.
    /// </summary>
    public const string MethodStartTag = "<!--*|MethodSTART|*-->";
    /// <summary>
    /// Id tag that use to know where the method information sections ends.
    /// </summary>
    public const string MethodEndTag = "<!--*|MethodEND|*-->";
    /// <summary>
    /// Id tag that use to know where the machine information sections starts.
    /// </summary>
    public const string MachineStartTag = "<!--*|MachineSTART|*-->";
    /// <summary>
    /// Id tag that use to know where the machine information sections ends.
    /// </summary>
    public const string MachineEndTag = "<!--*|MachineEND|*-->";
    /// <summary>
    /// Id tag that use to know where the application information sections starts.
    /// </summary>
    public const string ApplicationStartTag = "<!--*|ApplicationSTART|*-->";
    /// <summary>
    /// Id tag that use to know where the application information sections ends.
    /// </summary>
    public const string ApplicationEndTag = "<!--*|ApplicationEND|*-->";
    /// <summary>
    /// Id tag that use to know where the web application information sections starts.
    /// </summary>
    public const string WebApplicationStartTag = "<!--*|WebApplicationSTART|*-->";
    /// <summary>
    /// Id tag that use to know where the web application information sections ends.
    /// </summary>
    public const string WebApplicationEndTag = "<!--*|WebApplicationEND|*-->";
}