using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Database;
using Roy.Logging.Domain.Settings.Database;
using Roy.Logging.Extensions;
using System.Globalization;
using System.Text;

namespace Roy.Logging.Aspect.Database.Helpers;

/// <summary>
/// Query builder helper.
/// </summary>
internal class QueryBuilder
{
    /// <summary>
    /// Creates the query to run to insert the message in the database.
    /// </summary>
    /// <param name="message">
    /// Message detail to use for building the query.
    /// </param>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    /// <returns>
    /// Query.
    /// </returns>
    public StringBuilder Create(MessageDetail message, DatabaseSetting setting)
    {
        StringBuilder query = new StringBuilder(setting.Query);
        this.PopulateDatabaseValues(query, setting);
        if (message.IsExceptionType())
        {   
            this.PopulateExceptionDetails(query, message, 
                setting.Culture);
        }
        else
        {
            this.PopulateLogDetails(query, message,
                setting.Culture);
        }

        return query;
    }

    /// <summary>
    /// Populates the database values.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="setting">
    /// Database settings.
    /// </param>
    private void PopulateDatabaseValues(StringBuilder query, DatabaseSetting setting)
    {
        query.Replace(SchemaTag.DatabaseName, setting.DatabaseName);
        query.Replace(SchemaTag.TableName, setting.TableName);
    }

    /// <summary>
    /// Populate the values being shared by the exception and logs issues.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="record">
    /// Record used to populate the query.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void PopulateBasicValues(StringBuilder query, Record record, CultureInfo culture)
    {
        query.Replace(SchemaTag.Id, record.Id);
        query.Replace(SchemaTag.Date, record.Date.ToString(
            StringValues.LogDateFormat));
        query.Replace(SchemaTag.Level, record.Level
            .ToCurrentCultureString(culture));
        query.Replace(SchemaTag.Message, record.Message);
        query.Replace(SchemaTag.MachineCLRVersion, record.MachineCLRVersion);
        query.Replace(SchemaTag.MachineDomainName, record.MachineDomainName);
        query.Replace(SchemaTag.MachineName, record.MachineName);
        query.Replace(SchemaTag.MachineOperativeSystemVersion, 
            record.MachineOperativeSystemVersion);
        query.Replace(SchemaTag.MachineUserAccountName, record.MachineUserAccountName);
        query.Replace(SchemaTag.MachineOperativeSystem, record.MachineOperativeSystem);
        query.Replace(SchemaTag.MethodCallerFileName, record.MethodCallerFileName);
        query.Replace(SchemaTag.MethodCallerMethodName, record.MethodCallerMethodName);
        query.Replace(SchemaTag.MethodCallerLineNumber, 
            record.MethodCallerLineNumber.ToString());
        query.Replace(SchemaTag.ApplicationIsDebuggingEnabled, 
            record.ApplicationIsDebuggingEnabled.ToString());
        query.Replace(SchemaTag.ApplicationPhysicalPath, 
            record.ApplicationPhysicalPath.ToString());
        query.Replace(SchemaTag.ApplicationAssemblyLocation, record.ApplicationAssemblyLocation);
        query.Replace(SchemaTag.ApplicationFriendlyName, record.ApplicationFriendlyName);
        query.Replace(SchemaTag.ApplicationIsFullyTrusted, 
            record.ApplicationIsFullyTrusted.ToString());
        query.Replace(SchemaTag.ApplicationUserDomainName, record.ApplicationUserDomainName);
        query.Replace(SchemaTag.ApplicationUserName, record.ApplicationUserName);
        query.Replace(SchemaTag.WebApplicationCurrentURL, record.WebApplicationCurrentURL);
        query.Replace(SchemaTag.WebApplicationCurrentURLParameters, 
            record.WebApplicationCurrentURLParameters);
        query.Replace(SchemaTag.WebApplicationPreviousURL, record.WebApplicationPreviousURL);
        query.Replace(SchemaTag.WebApplicationUserHostIP, record.WebApplicationUserHostIP);
        query.Replace(SchemaTag.WebApplicationIsSecureConnection, 
            record.WebApplicationIsSecureConnection.ToString());
        query.Replace(SchemaTag.WebApplicationDomainName, record.WebApplicationDomainName);
        query.Replace(SchemaTag.WebApplicationCookiesValues, record.WebApplicationCookiesValues);
        query.Replace(SchemaTag.WebApplicationHeadersValues, record.WebApplicationHeadersValues);
        query.Replace(SchemaTag.WebApplicationUserLanguagePreferences, 
            record.WebApplicationUserLanguagePreferences);
        query.Replace(SchemaTag.CustomListOfParametersJSON, record.CustomListOfParametersJSON);
    }

    /// <summary>
    /// Populate the exception's details in the query.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="message">
    /// Object used to populate the query.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void PopulateExceptionDetails(StringBuilder query, 
        MessageDetail message, CultureInfo culture)
    {
        ExceptionRecord record = new ExceptionRecord((ExceptionDetail)message);
        this.PopulateBasicValues(query, record, culture);
        query.Replace(SchemaTag.ExceptionMessage, record.ExceptionMessage);
        query.Replace(SchemaTag.ExceptionStackTrace, record.ExceptionStackTrace);
        query.Replace(SchemaTag.ExceptionJSON, record.ExceptionJSON);
        query.Replace(SchemaTag.ExceptionSource, record.Source);
        query.Replace(SchemaTag.ExceptionHelpLink, record.HelpLink);
    }

    /// <summary>
    /// Populate the log's details in the query.
    /// </summary>
    /// <param name="query">
    /// String containing the query.
    /// </param>
    /// <param name="message">
    /// Object used to populate the query.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void PopulateLogDetails(StringBuilder query, 
        MessageDetail message, CultureInfo culture)
    {
        LogRecord record = new LogRecord((LogDetail)message);
        this.PopulateBasicValues(query, record, culture);
        query.Replace(SchemaTag.LogValueJSON, record.LogValueInJSONFormat);
    }
}