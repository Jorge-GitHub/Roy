using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.Program;
using Roy.Logging.Extensions;
using Roy.Logging.Resources.Languages.EmailTemplate;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

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
    /// <returns>
    /// Message to be send to the user.
    /// </returns>
    public void Decorate(StringBuilder body, MessageDetail bodyDetail, CultureInfo culture)
    {
        this.PopulateMessageDetails(body, bodyDetail, culture);
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
    private void PopulateMessageDetails(StringBuilder body, 
        MessageDetail bodyDetail, CultureInfo culture)
    {
        body.Replace(Tags.Id, bodyDetail.Id);
        body.Replace(Tags.Message, bodyDetail.Message);
        body.Replace(Tags.Date, bodyDetail.Date.ToString(
            StringValue.LogDateFormat));
        body.Replace(Tags.Level, bodyDetail.Level.ToCurrentCultureString(culture));
        body.Replace(Tags.CurrentYear, DateTime.Now.Year.ToString());
        this.PopulateMethodInformation(body, bodyDetail.StackFrame);
        this.PopulateMachineInformation(body, bodyDetail.MachineInformation, culture);
        this.PopulateApplicationInformation(body, bodyDetail, culture);
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
            this.TagHelper.CleanTagDetails(body, 
                this.TagHelper.ExceptionTraceTags);
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
            this.TagHelper.CleanTagDetails(body, this.TagHelper.MethodTags);
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
                    this.TagHelper.MachineTags);
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
            this.TagHelper.CleanTagDetails(body, this.TagHelper.MachineTags);
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
            this.PopulateApplicationInformation(body, bodyDetail.ApplicationInformation, 
                culture);
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
                    this.TagHelper.ApplicationTags);
            }
            else
            {
                body.Replace(Tags.AssemblyLocation, application.AssemblyLocation);
            }
        }
        else
        {
            this.TagHelper.CleanTagDetails(body, this.TagHelper.ApplicationTags);
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
    private void PopulateWebApplicationInformation(StringBuilder body, 
        WebApplication application, CultureInfo culture)
    {
        this.PopulateApplicationInformation(body, application, culture);
        if (application.FailedToLoad)
        {

        }
        else
        {

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

    }
}