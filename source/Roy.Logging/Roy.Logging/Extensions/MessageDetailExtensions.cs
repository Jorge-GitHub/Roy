using Roy.Logging.Domain.Attributes;

namespace Roy.Logging.Extensions;

/// <summary>
/// Message detail extensions.
/// </summary>
public static class MessageDetailExtensions
{
    /// <summary>
    /// Determinate whether the Message detail is an exception type or not.
    /// </summary>
    /// <param name="message">
    /// Message to check/validate.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the Message detail is an exception type or not.
    /// </returns>
    public static bool IsExceptionType(this MessageDetail message)
    {
        return message is ExceptionDetail;
    }
}