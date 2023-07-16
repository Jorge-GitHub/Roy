using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types.StringExtensions;
using Roy.Domain.Contants;
using System.Runtime.InteropServices;

namespace Roy.Logging.Aspect.SystemLog;

/// <summary>
/// Linux event logger.
/// </summary>
internal class LinuxEvent
{
    private enum Option
    {
        Pid = 0x01,
        Console = 0x02,
        Delay = 0x04,
        NoDelay = 0x08,
        NoWait = 0x10,
        PrintError = 0x20
    }

    private enum Facility
    {
        User = 1 << 3,
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
        if (level.Equals(Level.Error)
            || level.Equals(Level.Critical)
            || level.Equals(Level.Emergency)
            || level.Equals(Level.Alert))
        {
            lines.Span(line => Console.Error.WriteLine(line));
            this.WriteLinesOnSysLog(level, lines);
            return;
        }
        lines.Span(line => Console.WriteLine(line));
        this.WriteLinesOnSysLog(level, lines);
    }

    [DllImport("libc")]
    private static extern void openlog(IntPtr ident, Option option, Facility facility);

    [DllImport("libc")]
    private static extern void syslog(int priority, string message);

    [DllImport("libc")]
    private static extern void closelog();

    public void WriteLinesOnSysLog(Level level, List<string> lines)
    {
        IntPtr input = Marshal.StringToHGlobalAnsi(StringValues.Roy);
        openlog(input, Option.Console | Option.Pid | Option.PrintError, Facility.User);
        lines.Span(line => syslog((int)level, line.Trim()));
        closelog();
        Marshal.FreeHGlobal(input);
    }
}