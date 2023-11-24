using Avalon.Base.Extension.Collections;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Resources.Languages;
using System.Globalization;
using System.Text;

namespace Roy.Logging.Aspect.Email.Helpers;

/// <summary>
/// Tag decorator helper.
/// </summary>
internal class TagDecorator
{
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
    /// Get the language version for true or false.
    /// </summary>
    /// <param name="value">
    /// Flag that determinate whether to return true or false.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <returns>
    /// Language version for true or false.
    /// </returns>
    public string GetCultureTrueOrFalse(bool value, CultureInfo culture)
    {
        if (value)
        {
            return General.ResourceManager.GetString(
                GeneralLabel.True, culture);
        }

        return General.ResourceManager.GetString(
            GeneralLabel.False, culture);
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
