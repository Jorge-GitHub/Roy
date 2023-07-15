using Avalon.Base.Extension.Types;
using Roy.Domain.Attributes;
using Roy.Domain.Contants;
using Roy.Logging.Extensions;
using System.Diagnostics;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

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
            using (EventLog eventLog = new EventLog(
                StringValues.ApplicationLogName))
            {
                eventLog.Source = StringValues.ApplicationLogName;
                eventLog.WriteEntry(json.LimitLength(31000),
                   message.Level.ToEventLogEntryType());
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