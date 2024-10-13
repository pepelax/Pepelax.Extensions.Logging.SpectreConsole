using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Pepelax.Extensions.Logging.SpectreConsole;

public static class SpectreConsoleServicesExtensions
{
    public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";

    public static IServiceCollection AddSpectreLogging(this IServiceCollection pServices)
    {
        return pServices.AddLogging((configure) =>
        {
            configure.ClearProviders();

            var options = new SpectreConsoleLoggerOptions()
            {
                IncludeNewLineBeforeMessage = false,
                IncludeTimestamp = true,
                TimestampFormat = DATETIME_FORMAT,
                IncludeCategory = false,
                // UseFixedIndent = true,
                // FixedIndent = 0,
                LogException = true,
                IndentAfterNewLine = false,
                TimestampFormatter = (logopt, logbuild, time) =>
                {
                    // logbuild.Append("[grey on black]");
                    logbuild.Append(time.ToString(logopt.TimestampFormat, logopt.CultureInfo));
                    logbuild.Append(' ');
                    // logbuild.Append("[/] ");
                },
                LogLevelFormatter = (logopt, logbuild, logLevel) =>
                {
                    var lvl = logLevel switch
                    {
                        LogLevel.Trace => "[[[silver bold]TRCE[/]]]",
                        LogLevel.Debug => "[[[silver bold]DBUG[/]]]",
                        LogLevel.Information => "[[[green bold]INFO[/]]]",
                        LogLevel.Warning => "[[[yellow bold]WARN[/]]]",
                        LogLevel.Error => "[[[red bold]FAIL[/]]]",
                        LogLevel.Critical => "[[[red bold]CRIT[/]]]",
                        LogLevel.None => "[[[silver bold]NONE[/]]]",
                        _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
                    };
                    logbuild.Append(lvl);
                }
            };
            configure.AddSpectreConsole(options);
        });
    }

    public static void LogOut(this ILogger pLog,
        LogLevel pLevel,
        Exception? pException = null,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        var p = Path.GetFileName(pFile);

        var args = (pArgs is null) ? [] : pArgs;
        var msg = Markup.Escape(pMessage?.ToString() ?? string.Empty);

        pLog.LogMarkup(
            logLevel: pLevel,
            message: $"[yellow underline]{p}:{pLineNumber}[/] [fuchsia bold]{pCaller}[/] [[{Environment.CurrentManagedThreadId}]] [silver]->[/] {msg}",
            exception: pException,
            args: args);
    }

    public static void Debug(this ILogger pLog,
        Exception? pException = null,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.LogOut(
            pLevel: LogLevel.Debug,
            pMessage: pMessage,
            pException: pException,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Debug(this ILogger pLog,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.Debug(
            pMessage: pMessage,
            pException: null,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Info(this ILogger pLog,
        Exception? pException = null,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.LogOut(
            pLevel: LogLevel.Information,
            pMessage: pMessage,
            pException: pException,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Info(this ILogger pLog,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.Info(
            pMessage: pMessage,
            pException: null,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Warn(this ILogger pLog,
        Exception? pException = null,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.LogOut(
            pLevel: LogLevel.Warning,
            pMessage: pMessage,
            pException: pException,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Warn(this ILogger pLog,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.Warn(
            pMessage: pMessage,
            pException: null,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Error(this ILogger pLog,
        Exception? pException = null,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.LogOut(
            pLevel: LogLevel.Error,
            pMessage: pMessage,
            pException: pException,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Error(this ILogger pLog,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.Error(
            pMessage: pMessage,
            pException: null,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Crit(this ILogger pLog,
        Exception? pException = null,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.LogOut(
            pLevel: LogLevel.Critical,
            pMessage: pMessage,
            pException: pException,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Crit(this ILogger pLog,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.Crit(
            pMessage: pMessage,
            pException: null,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Trace(this ILogger pLog,
        Exception? pException = null,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.LogOut(
            pLevel: LogLevel.Trace,
            pMessage: pMessage,
            pException: pException,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }

    public static void Trace(this ILogger pLog,
        object? pMessage = null,
        object?[]? pArgs = null,
        [CallerLineNumber] int pLineNumber = 0,
        [CallerMemberName] string pCaller = "",
        [CallerFilePath] string pFile = "")
    {
        pLog.Trace(
            pMessage: pMessage,
            pException: null,
            pArgs: pArgs,
            pLineNumber: pLineNumber,
            pCaller: pCaller,
            pFile: pFile);
    }
}