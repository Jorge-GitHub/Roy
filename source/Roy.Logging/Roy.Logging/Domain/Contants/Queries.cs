namespace Roy.Logging.Domain.Contants;

/// <summary>
/// Queries used to save the issues (exceptions/logs) in the database.
/// </summary>
internal struct Queries
{
    /// <summary>
    /// Query to insert the exception into the database.
    /// </summary>
    public const string InsertExceptionQuery = @"
        INSERT INTO [*|DatabaseName|*].[dbo].[*|TableName|*]
           (
                [Id]
                ,[Level]
                ,[Message]
                ,[Date]
                ,[CustomListOfParametersJSON]
                ,[ExceptionMessage]
                ,[ExceptionStackTrace]
                ,[ExceptionParametersListJSON]
                ,[ExceptionTraceSource]
                ,[ExceptionTraceHelpLink]
                ,[ExceptionJSON]
                ,[MethodCallerFileName]
                ,[MethodCallerMethodName]
                ,[MethodCallerLineNumber]
                ,[MethodParametersJSON]
                ,[MachineCLRVersion]
                ,[MachineDomainName]
                ,[MachineName]
                ,[MachineOperativeSystemVersion]
                ,[MachineUserAccountName]
                ,[MachineOperativeSystem]
                ,[ApplicationIsDebuggingEnabled]
                ,[ApplicationPhysicalApplicationPath]
                ,[ApplicationAssemblyLocation]
                ,[ApplicationFriendlyName]
                ,[ApplicationIsFullyTrusted]
                ,[ApplicationUserDomainName]
                ,[ApplicationUserName]
                ,[WebApplicationCurrentURL]
                ,[WebApplicationCurrentURLParameters]
                ,[WebApplicationPreviousURL]
                ,[WebApplicationUserHostIP]
                ,[WebApplicationIsSecureConnection]
                ,[WebApplicationDomainName]
                ,[WebApplicationCookiesValues]
                ,[WebApplicationHeadersValues]
                ,[WebApplicationUserLanguagePreferences]
        VALUES
           (
                *|Id|*
                ,*|Level|*
                ,*|Message|*
                ,*|Date|*
                ,*|CustomListOfParametersJSON|*
                ,*|ExceptionMessage|*
                ,*|ExceptionStackTrace|*
                ,*|ExceptionParametersListJSON|*
                ,*|ExceptionTraceSource|*
                ,*|ExceptionTraceHelpLink|*
                ,*|ExceptionJSON|*
                ,*|MethodCallerFileName|*
                ,*|MethodCallerMethodName|*
                ,*|MethodCallerLineNumber|*
                ,*|MethodParametersJSON|*
                ,*|MachineCLRVersion|*
                ,*|MachineDomainName|*
                ,*|MachineName|*
                ,*|MachineOperativeSystemVersion|*
                ,*|MachineUserAccountName|*
                ,*|MachineOperativeSystem|*
                ,*|ApplicationIsDebuggingEnabled|*
                ,*|ApplicationPhysicalApplicationPath|*
                ,*|ApplicationAssemblyLocation|*
                ,*|ApplicationFriendlyName|*
                ,*|ApplicationIsFullyTrusted|*
                ,*|ApplicationUserDomainName|*
                ,*|ApplicationUserName|*
                ,*|WebApplicationCurrentURL|*
                ,*|WebApplicationCurrentURLParameters|*
                ,*|WebApplicationPreviousURL|*
                ,*|WebApplicationUserHostIP|*
                ,*|WebApplicationIsSecureConnection|*
                ,*|WebApplicationDomainName|*
                ,*|WebApplicationCookiesValues|*
                ,*|WebApplicationHeadersValues|*
                ,*|WebApplicationUserLanguagePreferences|*
            )
        GO";

    /// <summary>
    /// Query to insert the log into the database.
    /// </summary>
    public const string InsertLogQuery = @"
        INSERT INTO [*|DatabaseName|*].[dbo].[*|TableName|*]
            (
                [Id]
                ,[Level]
                ,[Message]
                ,[Date]
                ,[CustomListOfParametersJSON]
                ,[LogValueJSON]
                ,[MethodCallerFileName]
                ,[MethodCallerMethodName]
                ,[MethodCallerLineNumber]
                ,[MethodParametersJSON]
                ,[MachineCLRVersion]
                ,[MachineDomainName]
                ,[MachineName]
                ,[MachineOperativeSystemVersion]
                ,[MachineUserAccountName]
                ,[MachineOperativeSystem]
                ,[ApplicationIsDebuggingEnabled]
                ,[ApplicationPhysicalApplicationPath]
                ,[ApplicationAssemblyLocation]
                ,[ApplicationFriendlyName]
                ,[ApplicationIsFullyTrusted]
                ,[ApplicationUserDomainName]
                ,[ApplicationUserName]
                ,[WebApplicationCurrentURL]
                ,[WebApplicationCurrentURLParameters]
                ,[WebApplicationPreviousURL]
                ,[WebApplicationUserHostIP]
                ,[WebApplicationIsSecureConnection]
                ,[WebApplicationDomainName]
                ,[WebApplicationCookiesValues]
                ,[WebApplicationHeadersValues]
                ,[WebApplicationUserLanguagePreferences]
            )
        VALUES
            (
                *|Id|*
                ,*|Level|*
                ,*|Message|*
                ,*|Date|*
                ,*|CustomListOfParametersJSON|*
                ,*|LogValueJSON|*
                ,*|MethodCallerFileName|*
                ,*|MethodCallerMethodName|*
                ,*|MethodCallerLineNumber|*
                ,*|MethodParametersJSON|*
                ,*|MachineCLRVersion|*
                ,*|MachineDomainName|*
                ,*|MachineName|*
                ,*|MachineOperativeSystemVersion|*
                ,*|MachineUserAccountName|*
                ,*|MachineOperativeSystem|*
                ,*|ApplicationIsDebuggingEnabled|*
                ,*|ApplicationPhysicalApplicationPath|*
                ,*|ApplicationAssemblyLocation|*
                ,*|ApplicationFriendlyName|*
                ,*|ApplicationIsFullyTrusted|*
                ,*|ApplicationUserDomainName|*
                ,*|ApplicationUserName|*
                ,*|WebApplicationCurrentURL|*
                ,*|WebApplicationCurrentURLParameters|*
                ,*|WebApplicationPreviousURL|*
                ,*|WebApplicationUserHostIP|*
                ,*|WebApplicationIsSecureConnection|*
                ,*|WebApplicationDomainName|*
                ,*|WebApplicationCookiesValues|*
                ,*|WebApplicationHeadersValues|*
                ,*|WebApplicationUserLanguagePreferences|*
            )
        GO";
}