using System;
using System.Configuration;

namespace ConnectionTester.Api.Infrastructure;

/// <summary>
/// Reads the API's own configuration (appSettings section of PMUConnectionTester.Api.exe.config).
/// </summary>
internal static class ApiSettings
{
    public static int ListenPort { get; } = ReadInt("ListenPort", 8888);

    public static int MaxExecutionTimeoutSeconds { get; } = ReadInt("MaxExecutionTimeoutSeconds", 60);

    public static int DefaultMinimumFrames { get; } = ReadInt("DefaultMinimumFrames", 30);

    private static int ReadInt(string key, int defaultValue)
    {
        string value = ConfigurationManager.AppSettings[key];
        return int.TryParse(value, out int result) ? result : defaultValue;
    }

    /// <summary>
    /// Clamps a per-request timeout override so it can lower, but never exceed, the configured global ceiling.
    /// </summary>
    public static int ResolveTimeoutSeconds(int? requestedTimeoutSeconds) =>
        Math.Min(requestedTimeoutSeconds ?? MaxExecutionTimeoutSeconds, MaxExecutionTimeoutSeconds);

    public static int ResolveMinimumFrames(int? requestedMinimumFrames) =>
        requestedMinimumFrames ?? DefaultMinimumFrames;
}
