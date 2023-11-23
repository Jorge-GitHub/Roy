using Avalon.Base.Extension.Collections;
using Roy.Logging.Domain.Contants;
using System.Text;

namespace Roy.Logging.Aspect.Email.Helpers;

/// <summary>
/// Tag decorator helper.
/// </summary>
internal class TagDecorator
{
    /// <summary>
    /// Exception trace tags.
    /// </summary>
    public string[] ExceptionTraceTags = new string[] { Tags.Source, Tags.HelpLink };
    /// <summary>
    /// Method tags.
    /// </summary>
    public string[] MethodTags = new string[]
    {
        Tags.MethodCallerFileName, Tags.MethodCallerMethodName,
        Tags.MethodCallerLineNumber, Tags.MethodParametersJSON
    };
    /// <summary>
    /// Machine tags.
    /// </summary>
    public string[] MachineTags = new string[]
    {
        Tags.MachineCLRVersion, Tags.MachineDomainName,
        Tags.MachineName, Tags.MachineOperativeSystem,
        Tags.MachineOperativeSystemVersion, Tags.MachineUserAccountName
    };
    /// <summary>
    /// Application tags.
    /// </summary>
    public string[] ApplicationTags = new string[]
    {
        Tags.AssemblyLocation
    };
    /// <summary>
    /// Application tags.
    /// </summary>
    public string[] WebApplicationTags = new string[]
    {
    };
    /// <summary>
    /// Cleans the tags details.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="tags">
    /// List of tags to replace.
    /// </param>
    public void CleanTagDetails(StringBuilder body, params string[] tags)
    {
        this.SetTagDetails(body, string.Empty, tags);
        if (tags.HasElements())
        {
            foreach (string tag in tags)
            {
                body.Replace(tag, string.Empty);
            }
        }
    }

    /// <summary>
    /// Cleans the tags details.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="failedToLoadText">
    /// Failed to load text for the current language selected.
    /// </param>
    /// <param name="tags">
    /// List of tags to replace.
    /// </param>
    public void SetFailedToLoadTagDetails(StringBuilder body, string failedToLoadText,
        params string[] tags)
    {
        this.SetTagDetails(body, failedToLoadText, tags);
    }

    /// <summary>
    /// Set the tags details.
    /// </summary>
    /// <param name="body">
    /// String containing the tags to be replaced.
    /// </param>
    /// <param name="newValue">
    /// New value to set.
    /// </param>
    /// <param name="tags">
    /// List of tags to replace.
    /// </param>
    private void SetTagDetails(StringBuilder body, string newValue, params string[] tags)
    {
        if (tags.HasElements())
        {
            foreach (string tag in tags)
            {
                body.Replace(tag, newValue);
            }
        }
    }
}
