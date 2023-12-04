using Roy.Logging.Domain.Contants;
using Roy.Logging.Resources.Languages.EmailTemplate;
using System.Globalization;
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
    /// <param name="culture">
    /// Culture info.
    /// </param>
    public void Decorate(StringBuilder body, bool isAnException, CultureInfo culture)
    {
        body.Replace(EmailLabel.IssueDateTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.IssueDate, culture));
        body.Replace(EmailLabel.IssueLevelTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.IssueLevel, culture));
        body.Replace(EmailLabel.CustomMessageTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.CustomMessage, culture));
        body.Replace(EmailLabel.CustomListOfParametersJSONTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.CustomListOfParametersJSON, culture));
        body.Replace(EmailLabel.AssemblyLocationTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.AssemblyLocation, culture));
        body.Replace(EmailLabel.StackFrameInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.StackFrameInJSONFormat, culture));
        body.Replace(EmailLabel.FullIssueInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.FullIssueInJSONFormat, culture));
        body.Replace(EmailLabel.CopyrightTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.Copyright, culture));
        this.ReplaceMachineInformationLabels(body, culture);
        this.ReplaceMethodInformationLabels(body, culture);
        this.ReplaceApplicationInformationLabels(body, culture);

        if (isAnException)
        {
            this.ReplaceExceptionDetailsLabels(body, culture);
        }
        else
        {
            this.ReplaceLogDetailsLabels(body, culture);
        }
    }

    /// <summary>
    /// Replace the exception email labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void ReplaceExceptionDetailsLabels(StringBuilder body, CultureInfo culture)
    {
        body.Replace(EmailLabel.ExceptionInformationHeaderTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ExceptionInformationHeader, culture));
        body.Replace(EmailLabel.ErrorIdTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ErrorId, culture));
        body.Replace(EmailLabel.ExceptionMessageTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ExceptionMessage, culture));
        body.Replace(EmailLabel.SourceTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.Source, culture));
        body.Replace(EmailLabel.StackTraceTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.StackTrace, culture));
        body.Replace(EmailLabel.HelpLinkTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.HelpLink, culture));
        body.Replace(EmailLabel.FullExceptionInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.FullExceptionInJSONFormat, culture));
    }

    /// <summary>
    /// Replace the log email labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void ReplaceLogDetailsLabels(StringBuilder body, CultureInfo culture)
    {
        body.Replace(EmailLabel.LogInformationHeaderTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.LogInformationHeader, culture));
        body.Replace(EmailLabel.LogIdTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.LogId, culture));
        body.Replace(EmailLabel.ValueLoggedInJSONFormatTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ValueLoggedInJSONFormat, culture));
    }

    /// <summary>
    /// Replace the machine information labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void ReplaceMachineInformationLabels(StringBuilder body, CultureInfo culture)
    {
        body.Replace(EmailLabel.ServerInformationHeaderTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ServerInformationHeader, culture));
        body.Replace(EmailLabel.DomainNameTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.DomainName, culture));
        body.Replace(EmailLabel.CLRVersionTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.CLRVersion, culture));
        body.Replace(EmailLabel.ServerNameTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.ServerName, culture));
        body.Replace(EmailLabel.OperativeSystemTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.OperativeSystem, culture));
        body.Replace(EmailLabel.OperativeSystemTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.OperativeSystem, culture));
        body.Replace(EmailLabel.UserAccountNameTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.UserAccountName, culture));
        body.Replace(EmailLabel.OperativeSystemVersionTag, EmailLabels.ResourceManager
            .GetString(EmailLabel.OperativeSystemVersion, culture));
    }

    /// <summary>
    /// Replace the method information labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void ReplaceMethodInformationLabels(StringBuilder body, CultureInfo culture)
    {
        body.Replace(EmailLabel.MethodInformationHeaderTag, 
            EmailLabels.ResourceManager.GetString(
                EmailLabel.MethodInformationHeader, culture));
        body.Replace(EmailLabel.MethodCallerFileNameTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.MethodCallerFileName, culture));
        body.Replace(EmailLabel.MethodCallerMethodNameTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.MethodCallerMethodName, culture));
        body.Replace(EmailLabel.MethodCallerLineNumberTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.MethodCallerLineNumber, culture));
        body.Replace(EmailLabel.MethodParametersTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.MethodParameters, culture));
    }


    /// <summary>
    /// Replace the application information labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void ReplaceApplicationInformationLabels(StringBuilder body, CultureInfo culture)
    {
        body.Replace(EmailLabel.ApplicationInformationHeaderTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.ApplicationInformationHeader, culture));
        body.Replace(EmailLabel.ApplicationIsDebuggingEnabledTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.ApplicationIsDebuggingEnabled, culture));
        body.Replace(EmailLabel.ApplicationPhysicalPath,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.ApplicationPhysicalApplicationPath, culture));
        body.Replace(EmailLabel.ApplicationFriendlyNameTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.ApplicationFriendlyName, culture));
        body.Replace(EmailLabel.ApplicationIsFullyTrustedTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.ApplicationIsFullyTrusted, culture));
        body.Replace(EmailLabel.ApplicationUserDomainNameTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.ApplicationUserDomainName, culture));
        body.Replace(EmailLabel.ApplicationUserNameTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.ApplicationUserName, culture));
        this.ReplaceWebApplicationInformationLabels(body, culture);
    }

    /// <summary>
    /// Replace the web application information labels.
    /// </summary>
    /// <param name="body">
    /// String containing the HTML with the labels to be replaced.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    private void ReplaceWebApplicationInformationLabels(StringBuilder body, CultureInfo culture)
    {
        body.Replace(EmailLabel.WebApplicationCurrentURLTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationCurrentURL, culture));
        body.Replace(EmailLabel.WebApplicationCurrentURLParametersTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationCurrentURLParameters, culture));
        body.Replace(EmailLabel.WebApplicationPreviousURLTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationPreviousURL, culture));
        body.Replace(EmailLabel.WebApplicationUserHostIPTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationUserHostIP, culture));
        body.Replace(EmailLabel.WebApplicationIsSecureConnectionTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationIsSecureConnection, culture));
        body.Replace(EmailLabel.WebApplicationUserDomainNameTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationUserDomainName, culture));
        body.Replace(EmailLabel.WebApplicationUserLanguagePreferencesTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationUserLanguagePreferences, culture));
        body.Replace(EmailLabel.WebApplicationCookiesValuesTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationCookiesValues, culture));
        body.Replace(EmailLabel.WebApplicationHeadersValuesTag,
            EmailLabels.ResourceManager.GetString(
                EmailLabel.WebApplicationHeadersValues, culture));
    }

}