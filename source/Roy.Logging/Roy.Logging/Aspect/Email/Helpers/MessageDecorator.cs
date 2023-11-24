using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Extensions;
using Roy.Logging.Resources.Languages.EmailTemplate;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Roy.Logging.Aspect.Email.Helpers;

/// <summary>
/// Message decorator helper.
/// Replace the message details within the HTML body.
/// </summary>
internal class MessageDecorator
{
    /// <summary>
    /// Tag decorator helper.
    /// </summary>
    private TagDecorator TagHelper { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public MessageDecorator()
    {
        this.TagHelper = new TagDecorator();
    }
    /// <summary>
    /// Generates the body to be used for the message send to the user.
    /// </summary>
    /// <param name="content">
    /// Body's content containing the tags that will be replaced.
    /// </param>
    /// <param name="bodyDetail">
    /// Body detail to used for replacing the values.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <param name="settings">
    /// Log settings.
    /// </param>
    public void Decorate(StringBuilder body, MessageDetail bodyDetail, 
        CultureInfo culture, LogSetting settings)
    {
        this.PopulateMessageDetails(body, bodyDetail, culture, settings);
        if (bodyDetail is ExceptionDetail)
        {
            this.PopulateExceptionDetails(body, (ExceptionDetail)bodyDetail);
        }
        else
        {
            this.PopulateLogDetails(body, (LogDetail)bodyDetail);
        }
    }

    /// <summary>
    /// Populate the message details.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <param name="settings">
    /// Log settings.
    /// </param>
    private void PopulateMessageDetails(StringBuilder body, 
        MessageDetail bodyDetail, CultureInfo culture, LogSetting settings)
    {
        body.Replace(Tags.Id, bodyDetail.Id);
        body.Replace(Tags.Message, bodyDetail.Message);
        body.Replace(Tags.Date, bodyDetail.Date.ToString(
            StringValue.LogDateFormat));
        body.Replace(Tags.Level, bodyDetail.Level.ToCurrentCultureString(culture));
        body.Replace(Tags.CurrentYear, DateTime.Now.Year.ToString());
        this.PopulateInformationDetails(body, bodyDetail, culture, settings);
    }

    /// <summary>
    /// Populate the information details.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <param name="settings">
    /// Log settings.
    /// </param>
    private void PopulateInformationDetails(StringBuilder body,
        MessageDetail bodyDetail, CultureInfo culture, LogSetting settings)
    {
        if (settings.LogMethodInformation)
        {
            this.PopulateMethodInformation(body, bodyDetail.StackFrame);
        }
        else
        {
            this.RemoveMethodInformationSection(body);
        }
        if (settings.LogMachineInformation)
        {
            this.PopulateMachineInformation(body, bodyDetail.MachineInformation, culture);
        }
        else
        {
            this.RemoveMachineInformationSection(body);
        }
        if (settings.LogApplicationInformation)
        {
            this.PopulateApplicationInformation(body, bodyDetail, culture);
        }
        else
        {
            this.RemoveApplicationInformationSection(body);
        }
    }

    /// <summary>
    /// Populate the exception details.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="bodyDetail">
    /// Exception detail used to populate the body message.
    /// </param>
    private void PopulateExceptionDetails(StringBuilder body, ExceptionDetail bodyDetail)
    {
        body.Replace(Tags.ExceptionMessage, bodyDetail.ExceptionMessage);
        body.Replace(Tags.StackTrace, bodyDetail.StackTrace);
        body.Replace(Tags.ExceptionParametersListJSON,
            this.SerializeObject(bodyDetail.ListOfParameters));
        body.Replace(Tags.ExceptionJSON,
            this.SerializeObject(bodyDetail.ExceptionTrace));
        body.Replace(Tags.IssueJSON,
            this.SerializeObject(bodyDetail));

        if (bodyDetail.ExceptionTrace.IsNotNull())
        {
            body.Replace(Tags.Source, bodyDetail.ExceptionTrace.Source);
            body.Replace(Tags.HelpLink, bodyDetail.ExceptionTrace.HelpLink);
        }
        else
        {
            this.TagHelper.CleanTagDetails(body, TagsList.ExceptionTraceTags);
        }
    }

    /// <summary>
    /// Populate the log details.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="bodyDetail">
    /// Log detail used to populate the body message.
    /// </param>
    private void PopulateLogDetails(StringBuilder body, LogDetail bodyDetail)
    {
        body.Replace(Tags.IssueJSON,
            this.SerializeObject(bodyDetail));
        body.Replace(Tags.LogValueJSON,
            this.SerializeObject(bodyDetail.LogValue));
    }

    /// <summary>
    /// Serialize the object.
    /// </summary>
    /// <param name="objectDetail">
    /// Object to serialize.
    /// </param>
    /// <returns>
    /// Serialized version of the object.
    /// </returns>
    private string SerializeObject(object objectDetail)
    {
        try
        {
            if (objectDetail.IsNotNull())
            {
                return JsonSerializer.Serialize(objectDetail);
            }
        }
        catch { }

        return string.Empty;
    }

    /// <summary>
    /// Populate the method information.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="method">
    /// Method object to use to replace the tags.
    /// </param>
    private void PopulateMethodInformation(StringBuilder body, Method method)
    {
        if (method.IsNotNull())
        {
            body.Replace(Tags.MethodCallerFileName, method.CallerFileName);
            body.Replace(Tags.MethodCallerMethodName, method.CallerMethodName);
            body.Replace(Tags.MethodCallerLineNumber, method.CallerLineNumber.ToString());
            body.Replace(Tags.MethodParametersJSON, this.SerializeObject(method.Parameters));
        }
        else
        {
            this.TagHelper.CleanTagDetails(body, TagsList.MethodTags);
        }
    }

    /// <summary>
    /// Populate the machine information.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="computer">
    /// Computer object to use to replace the tags.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void PopulateMachineInformation(StringBuilder body, Machine computer,
        CultureInfo culture)
    {
        if (computer.IsNotNull())
        {
            if (computer.FailedToLoad)
            {
                TagHelper.SetFailedToLoadTagDetails(body,
                    EmailLabels.ResourceManager
                    .GetString(EmailLabel.FailedToLoad, culture),
                    TagsList.MachineTags);
            }
            else
            {
                body.Replace(Tags.MachineCLRVersion, computer.CLRVersion);
                body.Replace(Tags.MachineDomainName, computer.DomainName);
                body.Replace(Tags.MachineName, computer.Name);
                body.Replace(Tags.MachineOperativeSystem, computer.OperativeSystem);
                body.Replace(Tags.MachineOperativeSystemVersion, computer.OperativeSystemVersion);
                body.Replace(Tags.MachineUserAccountName, computer.UserAccountName);
            }
        }
        else
        {
            this.TagHelper.CleanTagDetails(body, TagsList.MachineTags);
        }
    }

    /// <summary>
    /// Populate application information.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="bodyDetail">
    /// Object used to populate the body message.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void PopulateApplicationInformation(StringBuilder body, MessageDetail bodyDetail,
        CultureInfo culture)
    {
        if (bodyDetail.WebApplicationInformation.IsNotNull())
        {
            this.PopulateWebApplicationInformation(body, 
                bodyDetail.WebApplicationInformation, culture);
        }
        else
        {
            this.PopulateApplicationInformation(body, 
                bodyDetail.ApplicationInformation, culture);
            this.CleanWebApplicationInformationDetails(body);
        }
    }

    /// <summary>
    /// Populate the application information.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="application">
    /// Application object to use to replace the tags.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void PopulateApplicationInformation(StringBuilder body, Application application,
        CultureInfo culture)
    {
        if (application.IsNotNull())
        {
            if (application.FailedToLoad)
            {
                TagHelper.SetFailedToLoadTagDetails(body,
                    EmailLabels.ResourceManager
                    .GetString(EmailLabel.FailedToLoad, culture),
                    TagsList.ApplicationTags);
            }
            else
            {
                body.Replace(Tags.AssemblyLocation, application.AssemblyLocation);
                body.Replace(Tags.ApplicationIsDebuggingEnabled, 
                    this.TagHelper.GetCultureTrueOrFalse(application.IsDebuggingEnabled, culture));
                body.Replace(Tags.ApplicationPhysicalApplicationPath, application.PhysicalApplicationPath);
                body.Replace(Tags.ApplicationFriendlyName, application.FriendlyName);
                body.Replace(Tags.ApplicationIsFullyTrusted,
                    this.TagHelper.GetCultureTrueOrFalse(application.IsFullyTrusted, culture));
                body.Replace(Tags.ApplicationUserDomainName, application.UserDomainName);
                body.Replace(Tags.ApplicationUserName, application.UserName);
            }
        }
        else
        {
            this.TagHelper.CleanTagDetails(body, TagsList.ApplicationTags);
        }
    }

    /// <summary>
    /// Populate the application information.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="webApplication">
    /// Application object to use to replace the tags.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void PopulateWebApplicationInformation(StringBuilder body, 
        WebApplication webApplication, CultureInfo culture)
    {
        this.PopulateApplicationInformation(body, webApplication, culture);
        if (webApplication.FailedToLoad)
        {
            TagHelper.SetFailedToLoadTagDetails(body,
                EmailLabels.ResourceManager
                .GetString(EmailLabel.FailedToLoad, culture),
                TagsList.WebApplicationTags);
        }
        else
        {
            body.Replace(Tags.WebApplicationCurrentURL, webApplication.CurrentURL);
            body.Replace(Tags.WebApplicationCurrentURLParameters, webApplication.CurrentURLParameters);
            body.Replace(Tags.WebApplicationPreviousURL, webApplication.PreviousURL);
            body.Replace(Tags.WebApplicationUserHostIP, webApplication.UserHostIP);
            body.Replace(Tags.WebApplicationIsSecureConnection,
                this.TagHelper.GetCultureTrueOrFalse(webApplication.IsSecureConnection, culture));
            body.Replace(Tags.WebApplicationUserDomainName, webApplication.UserDomainName);
            body.Replace(Tags.WebApplicationCookiesValues, webApplication.CookiesValues
                .ToStringStringBuilder().ToString());
            body.Replace(Tags.WebApplicationHeadersValues, webApplication.HeadersValues
                .ToStringStringBuilder().ToString());
            body.Replace(Tags.WebApplicationUserLanguagePreferences, webApplication.UserLanguagePreferences);
        }
    }

    /// <summary>
    /// Cleans the web application details tags
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    private void CleanWebApplicationInformationDetails(StringBuilder body)
    {
        this.TagHelper.CleanTagDetails(body, TagsList.WebApplicationTags);
    }

    /// <summary>
    /// Remove the method information section.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    private void RemoveMethodInformationSection(StringBuilder body)
    {

    }

    /// <summary>
    /// Remove the machine information section.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    private void RemoveMachineInformationSection(StringBuilder body)
    {

    }

    /// <summary>
    /// Remove the application information section.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    private void RemoveApplicationInformationSection(StringBuilder body)
    {
        this.RemoveWebApplicationInformationSection(body);
    }

    /// <summary>
    /// Remove the web application information section.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    private void RemoveWebApplicationInformationSection(StringBuilder body)
    {

    }
}