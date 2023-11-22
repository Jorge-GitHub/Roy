using Roy.Logging.Domain.Program;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Extensions;
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
            StringValues.LogDateFormat));
        body.Replace(Tags.AssemblyLocation, bodyDetail.AssemblyLocation);
        body.Replace(Tags.Level, bodyDetail.Level.ToCurrentCultureString(culture));
        body.Replace(Tags.CurrentYear, DateTime.Now.Year.ToString());
        body.Replace(Tags.StackFrameJSON,
            this.SerializeObject(bodyDetail.StackFrame));
        this.PopulateMachineInformation(body, bodyDetail.MachineInformation);
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

        if (bodyDetail.ExceptionTrace != null)
        {
            body.Replace(Tags.Source, bodyDetail.ExceptionTrace.Source);
            body.Replace(Tags.HelpLink, bodyDetail.ExceptionTrace.HelpLink);
        }
        else
        {
            this.CleanExceptionTraceDetails(body);
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
            if (objectDetail != null)
            {
                return JsonSerializer.Serialize(objectDetail);
            }
        }
        catch { }

        return string.Empty;
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
    private void PopulateMachineInformation(StringBuilder body, Machine computer)
    {
        if (computer != null)
        {
            body.Replace(Tags.MachineCLRVersion, computer.CLRVersion);
            body.Replace(Tags.MachineDomainName, computer.DomainName);
            body.Replace(Tags.MachineName, computer.Name);
            body.Replace(Tags.MachineOperativeSystem, computer.OperativeSystem);
            body.Replace(Tags.MachineOperativeSystemVersion, computer.OperativeSystemVersion);
            body.Replace(Tags.MachineUserAccountName, computer.UserAccountName);
        }
    }

    /// <summary>
    /// Cleans the exception trace details tags in case the
    /// exception trace is null.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    private void CleanExceptionTraceDetails(StringBuilder body)
    {
        body.Replace(Tags.Source, string.Empty);
        body.Replace(Tags.HelpLink, string.Empty);
    }
}