using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types.StringExtensions;
using Roy.Logging.Domain.Contants;
using System.Runtime.InteropServices;

namespace Roy.Logging.Aspect.SystemLog;

/// <summary>
/// Linux event logger.
/// </summary>
/// <remarks>
/// I based the Syslog logic section on the links below.
/// https://man7.org/linux/man-pages/man3/syslog.3.html
/// https://github.com/jitbit/SyslogCore/blob/main/Syslog.cs
/// </remarks>
internal class LinuxEvent
{
    /// <summary>
    /// The option argument to openlog() is a bit mask constructed by ORing.
    /// </summary>
    private enum SysLogOption
    {
        Pid = 0x01,
        Console = 0x02,
        Delay = 0x04,
        NoDelay = 0x08,
        NoWait = 0x10,
        PrintError = 0x20
    }

    /// <summary>
    /// The facility argument is used to specify what type of program is
    /// logging the message.This lets the configuration file specify
    /// that messages from different facilities will be handled
    /// differently.
    /// </summary>
    private enum SysLogFacility
    {
        User = 1 << 3,
    }

    /// <summary>
    /// This determines the importance of the message. The levels are,
    /// in order of decreasing importance.
    /// </summary>
    private enum SysLogLevel
    {
        Emergency = 0,
        Alert = 1,
        Critical = 2,
        Error = 3,
        Warning = 4,
        Notice = 5,
        Info = 6,
        Debug = 7
    }

    /// <summary>
    /// Log event on Linux.
    /// </summary>
    /// <param name="message">
    /// Message to save.
    /// </param>
    /// <param name="level">
    /// Issue level.
    /// </param>
    /// <remarks>
    /// On Linux/Unix logging is done by console output. this 
    /// output is redirect to a log file or the system logging console. 
    /// this is controlled by how the application is started.
    /// </remarks>
    public void Log(string message, Level level)
    {
        List<string> lines = message.SplitByBlocks(200);
        this.LogToConsole(lines, level);
        this.SysLog(lines, level);
    }

    /// <summary>
    /// Log to console.
    /// </summary>
    /// <param name="lines">
    /// Lines to log to console.
    /// </param>
    /// <param name="level">
    /// Issue level.
    /// </param>
    private void LogToConsole(List<string> lines, Level level)
    {
        if (level.Equals(Level.Error)
            || level.Equals(Level.Critical)
            || level.Equals(Level.Emergency)
            || level.Equals(Level.Alert))
        {
            lines.Span(line => Console.Error.WriteLine(line));
            return;
        }
        lines.Span(line => Console.WriteLine(line));
    }

    /// <summary>
    /// Log by using Linux Syslog.
    /// </summary>
    /// <param name="lines">
    /// Lines to log to console.
    /// </param>
    /// <param name="level">
    /// Issue level.
    /// </param>
    private void SysLog(List<string> lines, Level level)
    {
 
        this.WriteLinesOnSysLog(this.GetSysLogLevel(level), lines, StringValues.Roy);
    }

    /// <summary>
    /// Get the Syslog level.
    /// </summary>
    /// <param name="level">
    /// Issue level to used to get the Syslog.
    /// </param>
    /// <returns>
    /// SysLog level.
    /// </returns>
    private SysLogLevel GetSysLogLevel(Level level)
    {
        if (level.Equals(Level.Emergency))
        {
            return SysLogLevel.Emergency;
        }
        else if (level.Equals(Level.Alert))
        {
            return SysLogLevel.Alert;
        }
        else if (level.Equals(Level.Critical))
        {
            return SysLogLevel.Critical;
        }
        else if (level.Equals(Level.Error))
        {
            return SysLogLevel.Error;
        }
        else if (level.Equals(Level.Warning))
        {
            return SysLogLevel.Warning;
        }
        else if (level.Equals(Level.Notice))
        {
            return SysLogLevel.Notice;
        }
        else if (level.Equals(Level.Debug))
        {
            return SysLogLevel.Debug;
        }

        return SysLogLevel.Info;
    }

    /// <summary>
    /// opens a connection to the system logger for a program.
    /// </summary>
    /// <param name="ident">
    /// Pointer integer.
    /// </param>
    /// <param name="option">
    /// Syslog option.
    /// </param>
    /// <param name="facility">
    /// Syslog facility.
    /// </param>
    [DllImport("libc")]
    private static extern void openlog(IntPtr ident, SysLogOption option, SysLogFacility facility);

    /// <summary>
    /// syslog() generates a log message, which will be distributed 
    /// </summary>
    /// <param name="priority">
    /// Syslog level.
    /// </param>
    /// <param name="message">
    /// Message to log.
    /// </param>
    [DllImport("libc")]
    private static extern void syslog(int priority, string message);

    /// <summary>
    ///  closelog() closes the file descriptor being used to write to the
    ///  system logger.The use of closelog() is optional.
    /// </summary>
    [DllImport("libc")]
    private static extern void closelog();

    /// <summary>
    /// Write log to Syslog.
    /// </summary>
    /// <param name="level">
    /// Syslog level.
    /// </param>
    /// <param name="lines">
    /// Lines to write down.
    /// </param>
    /// <param name="applicationName">
    /// Application's name.
    /// </param>
    private void WriteLinesOnSysLog(SysLogLevel level, List<string> lines, 
        string applicationName)
    {
        try
        {
            IntPtr input = Marshal.StringToHGlobalAnsi(applicationName);
            openlog(input, SysLogOption.Console | SysLogOption.Pid
                | SysLogOption.PrintError, SysLogFacility.User);
            lines.Span(line => syslog((int)level, line.Trim()));
            closelog();
            Marshal.FreeHGlobal(input);
        }
        catch { }
    }
}