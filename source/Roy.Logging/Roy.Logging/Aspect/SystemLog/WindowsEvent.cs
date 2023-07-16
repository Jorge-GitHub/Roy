using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Extensions;
using System.Diagnostics;

namespace Roy.Logging.Aspect.SystemLog;

/// <summary>
/// Windows Event logs aspect.
/// </summary>
internal class WindowsEvent
{
    /// <summary>
    /// Log event on windows.
    /// </summary>
    /// <param name="message">
    /// Message to save.
    /// </param>
    /// <remarks>
    /// Windows maximum number of characters per event is around 32000.
    /// We limit it to 31000.
    /// </remarks>
    /// <param name="level">
    /// Issue level.
    /// </param>
    public void Log(string message, Level level)
    {
        EventLog eventLog = null;
        try
        {
            eventLog = new EventLog(StringValues.ApplicationLogName);
            eventLog.Source = StringValues.ApplicationLogName;
            eventLog.WriteEntry(message.LimitLength(31000),
               level.ToEventLogEntryType());
        }
        catch { }
        finally
        {
            if (eventLog != null)
            {
                eventLog.Close();
                eventLog.Dispose();
            }
        }
    }
}