using Roy.Logging.Domain.Contants;
using System.Globalization;

namespace Roy.Logging.Extensions;

/// <summary>
/// Language extensions.
/// </summary>
public static class LanguageExtensions
{
    /// <summary>
    /// Convert a language enum into a Culture info.
    /// </summary>
    /// <param name="language">
    /// Language to convert.
    /// </param>
    /// <returns>
    /// Culture info.
    /// </returns>
    public static CultureInfo ToCultureInfo(this Language language)
    {
        try
        {
            if (language.Equals(Language.English))
            {
                return new CultureInfo("en");
            }
            if (language.Equals(Language.German))
            {
                return new CultureInfo("de");
            }
            if (language.Equals(Language.Spanish))
            {
                return new CultureInfo("es");
            }
            if (language.Equals(Language.French))
            {
                return new CultureInfo("fr");
            }
        }
        catch { }
        return CultureInfo.DefaultThreadCurrentCulture
                    ?? new CultureInfo("en");
    }
}