using Avalon.Base.Extension.Types;
using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using System.Diagnostics;
using System.Text.Json;

namespace Roy.Logging.Helpers;

internal class SystemEventLogService
{
    /// <summary>
    /// Log the message into the system event log
    /// </summary>
    /// <param name="message">
    /// Message to save.
    /// </param>
    /// <remarks>
    /// Windows maximum number of characters per event is about 32000.
    /// We limit it to 31000 just in case.
    /// </remarks>
    public async void LogAsync(MessageDetail message)
    {
        string json = JsonSerializer.Serialize(message);
        if (OperatingSystem.IsWindows())
        {
            EventLog eventLog = null;
            try
            {
                eventLog = new EventLog();
                eventLog.Source = StringValues.ApplicationLogName;
                eventLog.WriteEntry(json.LimitLength(31000),
                    EventLogEntryType.Information);
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
}