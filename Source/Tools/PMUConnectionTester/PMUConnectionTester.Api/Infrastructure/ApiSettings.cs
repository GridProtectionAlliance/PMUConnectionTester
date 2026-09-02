using System;
using System.Configuration;

namespace ConnectionTester.Api.Infrastructure;

/// <summary>
/// Reads the API's own configuration (appSettings section of PMUConnectionTester.Api.exe.config).
/// </summary>
internal static class ApiSettings
{
    public static int DefaultMinimumFrames { get; } = ReadInt("DefaultMinimumFrames", 30);
    public static string ListenAddress { get; } = ReadString("ListenAddress", "localhost");

    public static int ListenPort { get; } = ReadInt("ListenPort", 8888);

    /// <summary>
    /// Default frame rate used for a live session when the request omits one.
    /// </summary>
    public static int LiveDefaultFrameRate { get; } = ReadInt("LiveDefaultFrameRate", 30);

    /// <summary>
    /// Ceiling applied to a live session's requested capture duration.
    /// </summary>
    public static int LiveMaxCaptureDurationSeconds { get; } = ReadInt("LiveMaxCaptureDurationSeconds", 120);

    /// <summary>
    /// Floor applied to a live session's requested minimum-communication-wait time.
    /// </summary>
    public static int LiveMinCommunicationWaitSeconds { get; } = ReadInt("LiveMinCommunicationWaitSeconds", 60);

    /// <summary>
    /// How long a finished/errored live session remains queryable before it's swept from the
    /// session store.
    /// </summary>
    public static int LiveSessionRetentionSeconds { get; } = ReadInt("LiveSessionRetentionSeconds", 300);

    public static int MaxExecutionTimeoutSeconds { get; } = ReadInt("MaxExecutionTimeoutSeconds", 60);

    /// <summary>
    /// Clamps a per-request capture-duration override so it can lower, but never exceed, the
    /// configured ceiling.
    /// </summary>
    public static int ResolveCaptureDurationSeconds(int? requestedSeconds) =>
        Math.Min(requestedSeconds ?? LiveMaxCaptureDurationSeconds, LiveMaxCaptureDurationSeconds);

    /// <summary>
    /// Clamps a per-request minimum-communication-wait override so it can raise, but never lower,
    /// the configured floor.
    /// </summary>
    public static int ResolveMinCommunicationWaitSeconds(int? requestedSeconds) =>
        Math.Max(requestedSeconds ?? LiveMinCommunicationWaitSeconds, LiveMinCommunicationWaitSeconds);

    public static int ResolveMinimumFrames(int? requestedMinimumFrames) =>
        requestedMinimumFrames ?? DefaultMinimumFrames;

    /// <summary>
    /// Clamps a per-request timeout override so it can lower, but never exceed, the configured
    /// global ceiling.
    /// </summary>
    public static int ResolveTimeoutSeconds(int? requestedTimeoutSeconds) =>
        Math.Min(requestedTimeoutSeconds ?? MaxExecutionTimeoutSeconds, MaxExecutionTimeoutSeconds);

    private static int ReadInt(string key, int defaultValue)
    {
        string value = ConfigurationManager.AppSettings[key];
        return int.TryParse(value, out int result) ? result : defaultValue;
    }

    private static string ReadString(string key, string defaultValue)
    {
        string value = ConfigurationManager.AppSettings[key];
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }
}