using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.System.Text;
using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Extensions;
using Roy.Logging.Resources.Languages.EmailTemplate;
using System.Globalization;
using System.Text;

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
        CultureInfo culture, InformationSetting settings)
    {
        this.PopulateMessageDetails(body, bodyDetail, culture, settings);
        if (bodyDetail.IsExceptionType())
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
        MessageDetail bodyDetail, CultureInfo culture, InformationSetting settings)
    {
        body.Replace(Tag.Id, bodyDetail.Id);
        body.Replace(Tag.Message, bodyDetail.Message);
        body.Replace(Tag.Date, bodyDetail.Date.ToString(
            StringValues.LogDateFormat));
        body.Replace(Tag.Level, bodyDetail.Level.ToCurrentCultureString(culture));
        body.Replace(Tag.CurrentYear, DateTime.Now.Year.ToString());
        body.Replace(Tag.CustomListOfParametersJSON, 
            bodyDetail.CustomListOfParameters.ToJSON());
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
        MessageDetail bodyDetail, CultureInfo culture, InformationSetting settings)
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
        body.Replace(Tag.ExceptionMessage, bodyDetail.ExceptionMessage);
        body.Replace(Tag.StackTrace, bodyDetail.StackTrace);
        body.Replace(Tag.ExceptionParametersListJSON, bodyDetail.ToJSON());
        body.Replace(Tag.ExceptionJSON, bodyDetail.ExceptionTrace.ToJSON());
        body.Replace(Tag.IssueJSON, bodyDetail.ToJSON());

        if (bodyDetail.ExceptionTrace.IsNotNull())
        {
            body.Replace(Tag.Source, bodyDetail.ExceptionTrace.Source);
            body.Replace(Tag.HelpLink, bodyDetail.ExceptionTrace.HelpLink);
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
        body.Replace(Tag.IssueJSON, bodyDetail.ToJSON());
        body.Replace(Tag.LogValueJSON,
            bodyDetail.LogValue.ToJSON());
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
            body.Replace(Tag.MethodCallerFileName, method.CallerFileName);
            body.Replace(Tag.MethodCallerMethodName, method.CallerMethodName);
            body.Replace(Tag.MethodCallerLineNumber, method.CallerLineNumber.ToString());
            body.Replace(Tag.MethodParametersJSON, method.ToJSON());
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
                body.Replace(Tag.MachineCLRVersion, computer.CLRVersion);
                body.Replace(Tag.MachineDomainName, computer.DomainName);
                body.Replace(Tag.MachineName, computer.Name);
                body.Replace(Tag.MachineOperativeSystem, computer.OperativeSystem);
                body.Replace(Tag.MachineOperativeSystemVersion, computer.OperativeSystemVersion);
                body.Replace(Tag.MachineUserAccountName, computer.UserAccountName);
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
                body.Replace(Tag.AssemblyLocation, application.AssemblyLocation);
                body.Replace(Tag.ApplicationIsDebuggingEnabled, 
                    this.TagHelper.GetCultureTrueOrFalse(application.IsDebuggingEnabled, culture));
                body.Replace(Tag.ApplicationPhysicalApplicationPath, application.PhysicalApplicationPath);
                body.Replace(Tag.ApplicationFriendlyName, application.FriendlyName);
                body.Replace(Tag.ApplicationIsFullyTrusted,
                    this.TagHelper.GetCultureTrueOrFalse(application.IsFullyTrusted, culture));
                body.Replace(Tag.ApplicationUserDomainName, application.UserDomainName);
                body.Replace(Tag.ApplicationUserName, application.UserName);
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
            body.Replace(Tag.WebApplicationCurrentURL, webApplication.CurrentURL);
            body.Replace(Tag.WebApplicationCurrentURLParameters, webApplication.CurrentURLParameters);
            body.Replace(Tag.WebApplicationPreviousURL, webApplication.PreviousURL);
            body.Replace(Tag.WebApplicationUserHostIP, webApplication.UserHostIP);
            body.Replace(Tag.WebApplicationIsSecureConnection,
                this.TagHelper.GetCultureTrueOrFalse(webApplication.IsSecureConnection, culture));
            body.Replace(Tag.WebApplicationUserDomainName, webApplication.UserDomainName);
            body.Replace(Tag.WebApplicationCookiesValues, webApplication.CookiesValues
                .ToStringStringBuilder().ToString());
            body.Replace(Tag.WebApplicationHeadersValues, webApplication.HeadersValues
                .ToStringStringBuilder().ToString());
            body.Replace(Tag.WebApplicationUserLanguagePreferences, webApplication.UserLanguagePreferences);
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
        body.RemoveBetweenTags(Tag.MethodStartTag, Tag.MethodEndTag);
    }

    /// <summary>
    /// Remove the machine information section.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    private void RemoveMachineInformationSection(StringBuilder body)
    {
        body.RemoveBetweenTags(Tag.MachineStartTag, Tag.MachineEndTag);
    }

    /// <summary>
    /// Remove the application information section.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    private void RemoveApplicationInformationSection(StringBuilder body)
    {
        body.RemoveBetweenTags(Tag.ApplicationStartTag, Tag.ApplicationEndTag);
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
        body.RemoveBetweenTags(Tag.WebApplicationStartTag, Tag.WebApplicationEndTag);
    }
}