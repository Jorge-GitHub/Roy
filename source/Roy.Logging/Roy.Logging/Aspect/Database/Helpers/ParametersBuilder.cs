using Avalon.Base.Extension.Data;
using Microsoft.Data.SqlClient;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Database;
using Roy.Logging.Extensions;

namespace Roy.Logging.Aspect.Database.Helpers;

/// <summary>
/// Parameters builder logic.
/// </summary>
internal class ParametersBuilder
{
    /// <summary>
    /// Get parameters list.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    /// <returns>
    /// Parameters list.
    /// </returns>
    public List<SqlParameter> GetParameters(MessageDetail message)
    {
        if (message.IsExceptionType())
        {
            return this.GetExceptionDetailParameters(message);
        }
        else
        {
            return this.GetLogDetailParameters(message);
        }
    }

    /// <summary>
    /// Get exception detail parameters list.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    /// <returns>
    /// Parameters list.
    /// </returns>
    private List<SqlParameter> GetExceptionDetailParameters(MessageDetail message)
    {
        ExceptionRecord record = new ExceptionRecord((ExceptionDetail)message);
        List<SqlParameter> parameters = this.GetMessageDetailParameters(record);
        parameters.Add(new SqlParameter(ParameterTag.ExceptionMessage, 
            record.ExceptionMessage.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ExceptionStackTrace, 
            record.ExceptionStackTrace.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ExceptionJSON, 
            record.ExceptionJSON.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ExceptionSource, 
            record.Source.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ExceptionHelpLink, 
            record.HelpLink.ToDBNullIfEmpty()));

        return parameters;
    }

    /// <summary>
    /// Get log detail parameters list.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    /// <returns>
    /// Parameters list.
    /// </returns>
    private List<SqlParameter> GetLogDetailParameters(MessageDetail message)
    {
        LogRecord record = new LogRecord((LogDetail)message);
        List<SqlParameter> parameters = this.GetMessageDetailParameters(record);
        parameters.Add(new SqlParameter(ParameterTag.LogValueJSON, 
            record.LogValueInJSONFormat.ToDBNullIfEmpty()));

        return parameters;
    }

    /// <summary>
    /// Get message detail parameters list.
    /// </summary>
    /// <param name="record">
    /// Record used to populate the query.
    /// </param>
    /// <returns>
    /// Parameters list.
    /// </returns>
    private List<SqlParameter> GetMessageDetailParameters(Record record)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter(ParameterTag.Id, record.Id));
        parameters.Add(new SqlParameter(ParameterTag.Date, record.Date));
        parameters.Add(new SqlParameter(ParameterTag.Level, record.Level));
        parameters.Add(new SqlParameter(ParameterTag.Message, 
            record.Message.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MachineCLRVersion, 
            record.MachineCLRVersion.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MachineDomainName, 
            record.MachineDomainName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MachineName, 
            record.MachineName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MachineOperativeSystemVersion,
            record.MachineOperativeSystemVersion.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MachineUserAccountName, 
            record.MachineUserAccountName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MachineOperativeSystem, 
            record.MachineOperativeSystem.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MethodCallerFileName, 
            record.MethodCallerFileName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MethodCallerMethodName, 
            record.MethodCallerMethodName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.MethodCallerLineNumber,
            record.MethodCallerLineNumber));
        parameters.Add(new SqlParameter(ParameterTag.MethodParametersJSON,
            record.MethodParametersJSON.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationIsDebuggingEnabled,
            record.ApplicationIsDebuggingEnabled));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationPhysicalPath,
            record.ApplicationPhysicalPath.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationAssemblyLocation, 
            record.ApplicationAssemblyLocation.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationFriendlyName, 
            record.ApplicationFriendlyName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationIsFullyTrusted,
            record.ApplicationIsFullyTrusted));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationUserDomainName, 
            record.ApplicationUserDomainName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationUserName, 
            record.ApplicationUserName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationCurrentURL, 
            record.WebApplicationCurrentURL.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationCurrentURLParameters,
            record.WebApplicationCurrentURLParameters.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationPreviousURL, 
            record.WebApplicationPreviousURL.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationUserHostIP, 
            record.WebApplicationUserHostIP.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationIsSecureConnection,
            record.WebApplicationIsSecureConnection));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationDomainName, 
            record.WebApplicationDomainName.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationCookiesValues, 
            record.WebApplicationCookiesValues.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationHeadersValues, 
            record.WebApplicationHeadersValues.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationUserLanguagePreferences,
            record.WebApplicationUserLanguagePreferences.ToDBNullIfEmpty()));
        parameters.Add(new SqlParameter(ParameterTag.CustomListOfParametersJSON, 
            record.CustomListOfParametersJSON.ToDBNullIfEmpty()));

        return parameters;
    }
}