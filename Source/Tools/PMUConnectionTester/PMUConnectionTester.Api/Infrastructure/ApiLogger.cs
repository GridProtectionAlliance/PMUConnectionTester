using System;
using System.IO;
using System.Reflection;
using GSF.Diagnostics;

namespace ConnectionTester.Api.Infrastructure;

/// <summary>
/// Writes start/params/result log lines for every test execution to a rolling daily text file
/// beside the executable, and forwards the same messages to GSF's diagnostics <see cref="LogPublisher"/>
/// so they participate in the platform's log routing if a subscriber is ever attached.
/// </summary>
internal static class ApiLogger
{
    private static readonly object FileLock = new();
    private static readonly LogPublisher Publisher = Logger.CreatePublisher(typeof(ApiLogger), MessageClass.Application);
    private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

    static ApiLogger()
    {
        Directory.CreateDirectory(LogDirectory);
    }

    public static void Start(Guid correlationId, string endpoint, string paramsJson)
    {
        WriteLine($"START endpoint={endpoint} correlationId={correlationId}");
        WriteLine($"PARAMS correlationId={correlationId} {paramsJson}");
    }

    public static void Result(Guid correlationId, string status, long elapsedMs, int frames, string message)
    {
        WriteLine($"RESULT correlationId={correlationId} status={status} frames={frames} elapsedMs={elapsedMs} message=\"{message}\"");
    }

    private static void WriteLine(string line)
    {
        string stamped = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} UTC | {line}";
        string logFile = Path.Combine(LogDirectory, $"api-{DateTime.UtcNow:yyyyMMdd}.log");

        lock (FileLock)
        {
            File.AppendAllText(logFile, stamped + Environment.NewLine);
        }

        Publisher.Publish(MessageLevel.Info, "PmuConnectionTest", line);
    }
}
