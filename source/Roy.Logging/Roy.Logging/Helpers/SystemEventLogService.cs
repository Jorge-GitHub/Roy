using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using Roy.Logging.Aspect.SystemLog;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Extensions;
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
    /// <param name="setting">
    /// Settings.
    /// </param>
    /// <remarks>
    /// Windows maximum number of characters per event is around 32000.
    /// We limit it to 31000.
    /// </remarks>
    public void LogAsync(MessageDetail message, IssueSetting setting)
    {
        if ((!setting.LevelsToLogOnSystemEvent.HasElements()
            || setting.LevelsToLogOnSystemEvent.Any(
                item => item.Equals(message.Level))))
        {
            string json = this.GetJSON(message);
            if (json.IsNotNullOrEmpty())
            {
                if (OperatingSystem.IsWindows())
                {
                    new WindowsEvent().Log(json, message.Level);
                }
                else if (OperatingSystem.IsLinux())
                {
                    new LinuxEvent().Log(json, message.Level);
                }
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
    private string GetJSON(MessageDetail message)
    {
        if(message.IsExceptionType())
        {
            return JsonSerializer.Serialize(message as ExceptionDetail);
        }

        return JsonSerializer.Serialize(message as LogDetail);
    }
}