using Roy.Domain.Contants;
using Roy.Logging.Resources.Languages;
using System.Globalization;

namespace Roy.Logging.Extensions;

public static class LevelExtensions
{
    /// <summary>
    /// Convert an issue level into its current culture (language) representation.
    /// </summary>
    /// <param name="level">
    /// Issue level to convert from.
    /// </param>
    /// <param name="culture">
    /// Culture info.
    /// </param>
    /// <returns>
    /// Current culture (language) representation of the issue level.
    /// </returns>
    public static string ToCurrentCultureString(this Level level, CultureInfo culture)
    {
        return LevelLabel.ResourceManager.GetString(
            level.ToString(), culture);
    }
}