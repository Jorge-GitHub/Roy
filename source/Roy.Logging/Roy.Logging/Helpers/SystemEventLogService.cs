using Avalon.Base.Extension.Types;
using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using Roy.Logging.Extensions;
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
    /// Windows maximum number of characters per event is around 32000.
    /// We limit it to 31000.
    /// </remarks>
    public async void LogAsync(MessageDetail message)
    {
        string json = this.GetJson(message);
        if (OperatingSystem.IsWindows())
        {
            this.LogEventOnWindows(json, message.Level);
        }
        else if(OperatingSystem.IsLinux())
        {

        }
    }

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
    private void LogEventOnWindows(string message, Level level)
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

    /// <summary>
    /// Get the JSON to log.
    /// </summary>
    /// <param name="message">
    /// Message to log.
    /// </param>
    /// <returns>
    /// JSON.
    /// </returns>
    private string GetJson(MessageDetail message)
    {
        if(message.IsExceptionType())
        {
            return JsonSerializer.Serialize(message as ExceptionDetail);
        }

        return JsonSerializer.Serialize(message as LogDetail);
    }
}