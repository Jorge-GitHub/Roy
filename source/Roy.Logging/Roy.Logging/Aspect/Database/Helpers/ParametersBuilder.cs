using Microsoft.Data.SqlClient;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Database;
using Roy.Logging.Extensions;
using System.Globalization;

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
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <returns>
    /// Parameters list.
    /// </returns>
    public List<SqlParameter> GetParameters(MessageDetail message, CultureInfo culture)
    {
        if (message.IsExceptionType())
        {
            return this.GetExceptionDetailParameters(message, culture);
        }
        else
        {
            return this.GetLogDetailParameters(message, culture);
        }
    }

    /// <summary>
    /// Get exception detail parameters list.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <returns>
    /// Parameters list.
    /// </returns>
    private List<SqlParameter> GetExceptionDetailParameters(
        MessageDetail message, CultureInfo culture)
    {
        ExceptionRecord record = new ExceptionRecord((ExceptionDetail)message);
        List<SqlParameter> parameters = this.GetMessageDetailParameters(record, culture);
        parameters.Add(new SqlParameter(ParameterTag.ExceptionMessage, 
            record.ExceptionMessage));
        parameters.Add(new SqlParameter(ParameterTag.ExceptionStackTrace, 
            record.ExceptionStackTrace));
        parameters.Add(new SqlParameter(ParameterTag.ExceptionJSON, 
            record.ExceptionJSON));
        parameters.Add(new SqlParameter(ParameterTag.ExceptionSource, 
            record.Source));
        parameters.Add(new SqlParameter(ParameterTag.ExceptionHelpLink, 
            record.HelpLink));

        return parameters;
    }

    /// <summary>
    /// Get log detail parameters list.
    /// </summary>
    /// <param name="message">
    /// Message's details.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <returns>
    /// Parameters list.
    /// </returns>
    private List<SqlParameter> GetLogDetailParameters(
        MessageDetail message, CultureInfo culture)
    {
        LogRecord record = new LogRecord((LogDetail)message);
        List<SqlParameter> parameters = this.GetMessageDetailParameters(record, culture);
        parameters.Add(new SqlParameter(ParameterTag.LogValueJSON, 
            record.LogValueInJSONFormat));

        return parameters;
    }

    /// <summary>
    /// Get message detail parameters list.
    /// </summary>
    /// <param name="record">
    /// Record used to populate the query.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <returns>
    /// Parameters list.
    /// </returns>
    private List<SqlParameter> GetMessageDetailParameters(
        Record record, CultureInfo culture)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter(ParameterTag.Id, record.Id));
        parameters.Add(new SqlParameter(ParameterTag.Date, record.Date));
        parameters.Add(new SqlParameter(ParameterTag.Level, record.Level
            .ToCurrentCultureString(culture)));
        parameters.Add(new SqlParameter(ParameterTag.
            Message, record.Message));
        parameters.Add(new SqlParameter(ParameterTag.
            MachineCLRVersion, record.MachineCLRVersion));
        parameters.Add(new SqlParameter(ParameterTag.MachineDomainName, 
            record.MachineDomainName));
        parameters.Add(new SqlParameter(ParameterTag.MachineName, 
            record.MachineName));
        parameters.Add(new SqlParameter(ParameterTag.MachineOperativeSystemVersion,
            record.MachineOperativeSystemVersion));
        parameters.Add(new SqlParameter(ParameterTag.MachineUserAccountName, 
            record.MachineUserAccountName));
        parameters.Add(new SqlParameter(ParameterTag.MachineOperativeSystem, 
            record.MachineOperativeSystem));
        parameters.Add(new SqlParameter(ParameterTag.MethodCallerFileName, 
            record.MethodCallerFileName));
        parameters.Add(new SqlParameter(ParameterTag.MethodCallerMethodName, 
            record.MethodCallerMethodName));
        parameters.Add(new SqlParameter(ParameterTag.MethodCallerLineNumber,
            record.MethodCallerLineNumber));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationIsDebuggingEnabled,
            record.ApplicationIsDebuggingEnabled));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationPhysicalPath,
            record.ApplicationPhysicalPath));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationAssemblyLocation, 
            record.ApplicationAssemblyLocation));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationFriendlyName, 
            record.ApplicationFriendlyName));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationIsFullyTrusted,
            record.ApplicationIsFullyTrusted));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationUserDomainName, 
            record.ApplicationUserDomainName));
        parameters.Add(new SqlParameter(ParameterTag.ApplicationUserName, 
            record.ApplicationUserName));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationCurrentURL, 
            record.WebApplicationCurrentURL));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationCurrentURLParameters,
            record.WebApplicationCurrentURLParameters));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationPreviousURL, 
            record.WebApplicationPreviousURL));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationUserHostIP, 
            record.WebApplicationUserHostIP));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationIsSecureConnection,
            record.WebApplicationIsSecureConnection));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationDomainName, 
            record.WebApplicationDomainName));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationCookiesValues, 
            record.WebApplicationCookiesValues));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationHeadersValues, 
            record.WebApplicationHeadersValues));
        parameters.Add(new SqlParameter(ParameterTag.WebApplicationUserLanguagePreferences,
            record.WebApplicationUserLanguagePreferences));
        parameters.Add(new SqlParameter(ParameterTag.CustomListOfParametersJSON, 
            record.CustomListOfParametersJSON));

        return parameters;
    }
}