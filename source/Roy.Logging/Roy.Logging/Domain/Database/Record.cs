using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Program;

namespace Roy.Logging.Domain.Database;

/// <summary>
/// Record to save in the database.
/// </summary>
internal class Record
{
    /// <summary>
    /// Id.
    /// </summary>
    public string Id { get; private set; }
    /// <summary>
    /// Date time.
    /// </summary>
    public DateTime Date { get; private set; }
    /// <summary>
    /// Exception level.
    /// </summary>
    public string Level { get; private set; }
    /// <summary>
    /// Log message.
    /// </summary>
    public string Message { get; private set; }
    /// <summary>
    /// .NET CLR Version.
    /// </summary>
    public string MachineCLRVersion { get; private set; }
    /// <summary>
    /// Domain name.
    /// </summary>
    public string MachineDomainName { get; private set; }
    /// <summary>
    /// Machine's name.
    /// </summary>
    public string MachineName { get; private set; }
    /// <summary>
    /// Operative system version.
    /// </summary>
    public string MachineOperativeSystemVersion { get; private set; }
    /// <summary>
    /// User account name.
    /// </summary>
    public string MachineUserAccountName { get; private set; }
    /// <summary>
    /// Operative system.
    /// </summary>
    public string MachineOperativeSystem { get; private set; }
    /// <summary>
    /// File name containing the code being executed. This
    /// information is normally extracted from the debugging symbols
    /// for the executable.
    /// </summary>
    public string MethodCallerFileName { get; private set; }
    /// <summary>
    /// Method name making the call.
    /// </summary>
    public string MethodCallerMethodName { get; private set; }
    /// <summary>
    /// Line number in the file containing the code being executed.
    /// This information is normally extracted from the debugging symbols
    /// for the executable.
    /// </summary>
    public int MethodCallerLineNumber { get; private set; }
    /// <summary>
    /// Parameters in JSON format.
    /// </summary>
    public string MethodParametersJSON { get; private set; }
    /// <summary>
    /// Flag that indicates whether the current 
    /// HTTP request is in debug mode
    /// </summary>
    public bool ApplicationIsDebuggingEnabled { get; private set; }
    /// <summary>
    /// Physical file system of the current 
    /// executing server application's root directory.
    /// </summary>
    public string ApplicationPhysicalPath { get; private set; }
    /// <summary>
    /// Assembly that the current code is running from.
    /// </summary>
    public string ApplicationAssemblyLocation { get; private set; }
    /// <summary>
    /// Application friendly name.
    /// </summary>
    public string ApplicationFriendlyName { get; private set; }
    /// <summary>
    /// Flag that determinate whether the application is executing in full trust.
    /// </summary>
    public bool ApplicationIsFullyTrusted { get; private set; }
    /// <summary>
    /// Network domain name associated with the current user.
    /// </summary>
    public string ApplicationUserDomainName { get; private set; }
    /// <summary>
    /// User name of the current thread.
    /// </summary>
    public string ApplicationUserName { get; private set; }
    /// <summary>
    /// Current URL.
    /// </summary>
    public string WebApplicationCurrentURL { get; private set; }
    /// <summary>
    /// Current page parameters.
    /// </summary>
    public string WebApplicationCurrentURLParameters { get; private set; }
    /// <summary>
    /// Previous URL.
    /// </summary>
    public string WebApplicationPreviousURL { get; private set; }
    /// <summary>
    /// User's Host's IP.
    /// </summary>
    public string WebApplicationUserHostIP { get; private set; }
    /// <summary>
    /// Flag that indicates whether the application is running in a secure connection.
    /// </summary>
    public bool WebApplicationIsSecureConnection { get; private set; }
    /// <summary>
    /// Domain's name.
    /// </summary>
    public string WebApplicationDomainName { get; private set; }
    /// <summary>
    /// Cookies values.
    /// </summary>
    public string WebApplicationCookiesValues { get; set; }
    /// <summary>
    /// Headers values.
    /// </summary>
    public string WebApplicationHeadersValues { get; set; }
    /// <summary>
    /// User preference languages.
    /// </summary>
    public string WebApplicationUserLanguagePreferences { get; set; }
    /// <summary>
    /// list of parameters.
    /// </summary>
    public string CustomListOfParametersJSON { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    public Record(MessageDetail message)
    {
        this.InitializeObject(message);
    }

    /// <summary>
    /// Initialize the object.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    private void InitializeObject(MessageDetail message)
    {
        this.Id = message.Id.LimitLength(32);
        this.Date = message.Date;
        this.Level = message.Level.ToString().LimitLength(11);
        this.Message = message.Message;
        this.CustomListOfParametersJSON = message.CustomListOfParameters.ToJSON();
        this.InitializeMachineInformation(message.MachineInformation);
        this.InitializeMethodInformation(message.StackFrame);
        this.InitializeApplicationInformation(message);
    }

    /// <summary>
    /// Initialize the machine information.
    /// </summary>
    /// <param name="machine">
    /// Machine/Server information.
    /// </param>
    private void InitializeMachineInformation(Machine machine)
    {
        if(machine.IsNotNull())
        {
            this.MachineCLRVersion = machine.CLRVersion.LimitLength(50);
            this.MachineDomainName = machine.DomainName.LimitLength(500);
            this.MachineName = machine.Name.LimitLength(500);
            this.MachineOperativeSystem = machine.OperativeSystem.LimitLength(10);
            this.MachineOperativeSystemVersion = machine.OperativeSystemVersion.LimitLength(250);
            this.MachineUserAccountName = machine.UserAccountName.LimitLength(250);
        }
    }

    /// <summary>
    /// Initialize the method information.
    /// </summary>
    /// <param name="method">
    /// Method information.
    /// </param>
    private void InitializeMethodInformation(Method method)
    {
        if(method.IsNotNull())
        {
            this.MethodCallerFileName = method.CallerFileName.LimitLength(1000);
            this.MethodCallerLineNumber = method.CallerLineNumber;
            this.MethodCallerMethodName = method.CallerMethodName.LimitLength(500);
            this.MethodParametersJSON = method.Parameters.ToJSON();
        }
    }

    /// <summary>
    /// Initialize the application information.
    /// </summary>
    /// <param name="application">
    /// Application information.
    /// </param>
    private void InitializeApplicationInformation(MessageDetail message)
    {
        if (message.WebApplicationInformation.IsNotNull())
        {
            this.InitializeWebApplicationInformation(message.WebApplicationInformation);
        }
        else
        {
            this.LoadApplicationInformation(message.ApplicationInformation);
        }
    }

    /// <summary>
    /// Initialize the web application information.
    /// </summary>
    /// <param name="webApplication">
    /// Web application information.
    /// </param>
    private void InitializeWebApplicationInformation(WebApplication webApplication)
    {
        if (webApplication.IsNotNull())
        {
            this.LoadApplicationInformation(webApplication);
            this.WebApplicationCookiesValues = webApplication.CookiesValues
                .ToStringStringBuilder().ToString().LimitLength(1000);
            this.WebApplicationCurrentURL = webApplication.CurrentURL.LimitLength(1000);
            this.WebApplicationCurrentURLParameters = webApplication.CurrentURLParameters
                .LimitLength(1000);
            this.WebApplicationDomainName = webApplication.DomainName.LimitLength(500);
            this.WebApplicationHeadersValues = webApplication.HeadersValues
                .ToStringStringBuilder().ToString().LimitLength(1000);
            this.WebApplicationIsSecureConnection = webApplication.IsSecureConnection;
            this.WebApplicationPreviousURL = webApplication.PreviousURL.LimitLength(1000);
            this.WebApplicationUserHostIP = webApplication.UserHostIP.LimitLength(250);
            this.WebApplicationUserLanguagePreferences = webApplication.UserLanguagePreferences
                .LimitLength(100);
        }
    }

    /// <summary>
    /// Loads the application information.
    /// </summary>
    /// <param name="application">
    /// Application information.
    /// </param>
    private void LoadApplicationInformation(Application application)
    {
        this.ApplicationIsDebuggingEnabled = application.IsDebuggingEnabled;
        this.ApplicationAssemblyLocation = application.AssemblyLocation.LimitLength(500);
        this.ApplicationFriendlyName = application.FriendlyName.LimitLength(100);
        this.ApplicationIsFullyTrusted = application.IsFullyTrusted;
        this.ApplicationUserDomainName = application.UserDomainName.LimitLength(500);
        this.ApplicationUserName = application.UserName.LimitLength(250);
        this.ApplicationPhysicalPath = application.PhysicalApplicationPath.LimitLength(500);
    }
}