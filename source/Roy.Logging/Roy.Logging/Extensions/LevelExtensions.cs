using Roy.Domain.Contants;
using Roy.Logging.Resources.Languages;
using System.Diagnostics;
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

    /// <summary>
    /// Converts an issue level into an event log entry type.
    /// </summary>
    /// <param name="level">
    /// Issue level to convert from.
    /// </param>
    /// <returns>
    /// Event log entry type.
    /// </returns>
    public static EventLogEntryType ToEventLogEntryType(this Level level)
    {
        if (level.Equals(Level.Error)
            || level.Equals(Level.Critical)
            || level.Equals(Level.Emergency)
            || level.Equals(Level.Alert))
        {
            return EventLogEntryType.Error;
        }
        if (level.Equals(Level.Warning))
        {
            return EventLogEntryType.Warning;
        }

        return EventLogEntryType.Information;
    }
}