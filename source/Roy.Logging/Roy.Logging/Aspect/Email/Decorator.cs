using Roy.Logging.Aspect.Email.Helpers;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Attributes;
using System.Globalization;
using System.Text;

namespace Roy.Logging.Aspect.Email;

/// <summary>
/// Email body decorator.
/// </summary>
internal class Decorator
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
    /// <param name="settings">
    /// Log settings.
    /// </param>
    /// <returns>
    /// Message to be send to the user.
    /// </returns>
    public string GenerateBody(string content, MessageDetail bodyDetail, 
        CultureInfo culture, LogSetting settings)
    {
        StringBuilder body = new StringBuilder(content);
        new LabelDecorator().Decorate(body, bodyDetail is ExceptionDetail, culture);
        new MessageDecorator().Decorate(body, bodyDetail, culture, settings);

        return body.ToString();
    }
}