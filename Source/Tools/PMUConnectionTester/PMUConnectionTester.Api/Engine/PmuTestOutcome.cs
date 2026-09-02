using System;
using GSF.PhasorProtocols;

namespace ConnectionTester.Api.Engine;

/// <summary>
/// Reason the engine's wait gate completed.
/// </summary>
internal enum CompletionReason
{
    MinimumFramesReached,
    ParsingExceptionThresholdExceeded,
    ConnectionError,
    PlaybackEnded,
    Timeout
}

/// <summary>
/// Raw result of a <see cref="PmuConnectionTestEngine"/> run - the controller maps this onto the
/// public response DTOs (<c>ConfigurationInfo</c> / <c>DataMetrics</c>).
/// </summary>
internal class PmuTestOutcome
{
    public CompletionReason Completion { get; set; }

    public bool ConfigurationFrameReceived { get; set; }

    public IConfigurationFrame ConfigurationFrame { get; set; }

    public long TotalFramesReceived { get; set; }

    public int DataFramesReceived { get; set; }

    public long TotalMissingFrames { get; set; }

    public long TotalCrcExceptions { get; set; }

    public int ParsingExceptions { get; set; }

    public double CalculatedFrameRate { get; set; }

    public bool IsConnected { get; set; }

    public int MinimumFramesRequired { get; set; }

    public long ElapsedMilliseconds { get; set; }

    public Exception LastError { get; set; }
}
