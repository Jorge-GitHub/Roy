using Avalon.Base.Extension.Types;
using Roy.Domain.Attributes;
using Roy.Logging.Aspect.SystemLog;
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
    /// <remarks>
    /// Windows maximum number of characters per event is around 32000.
    /// We limit it to 31000.
    /// </remarks>
    public async void LogAsync(MessageDetail message)
    {
        try
        {
            string json = this.GetJson(message);
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
        catch { }
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