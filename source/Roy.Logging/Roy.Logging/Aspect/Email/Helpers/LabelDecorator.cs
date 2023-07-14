using Roy.Domain.Contants;
using Roy.Logging.Resources.Languages.EmailTemplate;
using System.Text;

namespace Roy.Logging.Aspect.Email.Helpers;

/// <summary>
/// Decorator helper.
/// Replace the labels within the HTML with the specific 
/// ones for the current language culture.
/// </summary>
internal class LabelDecorator
{
    /// <summary>
    /// Replace the email labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    /// <param name="isAnException">
    /// Flag that determinate whether this is a HTML exception message or not.
    /// </param>
    public void Decorate(StringBuilder body, bool isAnException)
    {
        body.Replace(EmailLabel.IssueDateTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.IssueDate));
        body.Replace(EmailLabel.IssueLevelTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.IssueLevelTag));
        body.Replace(EmailLabel.CustomMessageTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.CustomMessage));
        body.Replace(EmailLabel.AssemblyLocationTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.AssemblyLocation));
        body.Replace(EmailLabel.AssemblyLocationTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.AssemblyLocation));
        body.Replace(EmailLabel.StackFrameInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.StackFrameInJSONFormat));
        body.Replace(EmailLabel.FullIssueInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.FullIssueInJSONFormat));
        body.Replace(EmailLabel.CopyrightTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.Copyright));

        this.ReplaceMachineInformationLabels(body);

        if (isAnException)
        {
            this.ReplaceExceptionDetailsLabels(body);
        }
        else
        {
            this.ReplaceLogDetailsLabels(body);
        }
    }

     /// <summary>
    /// Replace the exception email labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    private void ReplaceExceptionDetailsLabels(StringBuilder body)
    {
        body.Replace(EmailLabel.ExceptionInformationHeaderTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ExceptionInformationHeader));
        body.Replace(EmailLabel.ErrorIdTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ErrorId));
        body.Replace(EmailLabel.ExceptionMessageTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ExceptionMessage));
        body.Replace(EmailLabel.ParametersListInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ParametersListInJSONFormat));
        body.Replace(EmailLabel.SourceTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.Source));
        body.Replace(EmailLabel.StackTraceTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.StackTrace));
        body.Replace(EmailLabel.HelpLinkTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.HelpLink));
        body.Replace(EmailLabel.FullExceptionInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.FullExceptionInJSONFormat));
    }

    /// <summary>
    /// Replace the log email labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    private void ReplaceLogDetailsLabels(StringBuilder body)
    {
        body.Replace(EmailLabel.LogInformationHeaderTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.LogInformationHeader));
        body.Replace(EmailLabel.LogIdTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.LogId));
        body.Replace(EmailLabel.ValueLoggedInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ValueLoggedInJSONFormat));
    }

    /// <summary>
    /// Replace the machine information labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    private void ReplaceMachineInformationLabels(StringBuilder body)
    {
        body.Replace(EmailLabel.ServerInformationHeaderTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ServerInformationHeader));
        body.Replace(EmailLabel.DomainNameTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.DomainName));
        body.Replace(EmailLabel.CLRVersionTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.CLRVersion));
        body.Replace(EmailLabel.ServerNameTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ServerName));
        body.Replace(EmailLabel.OperativeSystemTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.OperativeSystem));
        body.Replace(EmailLabel.OperativeSystemTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.OperativeSystem));
        body.Replace(EmailLabel.UserAccountNameTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.UserAccountName));
    }
}