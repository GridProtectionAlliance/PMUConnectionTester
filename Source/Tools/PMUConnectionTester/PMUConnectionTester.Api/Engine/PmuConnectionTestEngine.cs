using System;
using System.Diagnostics;
using System.Threading;
using GSF;
using GSF.Communication;
using GSF.PhasorProtocols;

namespace ConnectionTester.Api.Engine;

/// <summary>
/// Headless orchestration of <see cref="MultiProtocolFrameParser"/> for a single synchronous connectivity
/// test run. This mirrors the setup performed by the WinForms app's <c>Connect()</c> method, but blocks
/// the caller until a pass/fail outcome is determined instead of driving a UI.
/// </summary>
internal class PmuConnectionTestEngine : IPmuConnectionTestEngine
{
    public PmuTestOutcome Run(PmuTestRequest request)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        ManualResetEventSlim completionGate = new(false);

        CompletionReason completion = CompletionReason.Timeout;
        int completedFlag = 0;
        Exception lastError = null;
        int dataFrameCount = 0;
        int parsingExceptionCount = 0;
        bool configurationFrameReceived = false;
        IConfigurationFrame configurationFrame = null;

        using MultiProtocolFrameParser parser = new();

        void Complete(CompletionReason reason)
        {
            // First signal wins - later events arriving after the gate is set are ignored
            if (Interlocked.CompareExchange(ref completedFlag, 1, 0) == 0)
                completion = reason;

            completionGate.Set();
        }

        parser.ReceivedConfigurationFrame += (_, e) =>
        {
            configurationFrame = e.Argument;
            configurationFrameReceived = true;
        };

        parser.ReceivedDataFrame += (_, _) =>
        {
            int count = Interlocked.Increment(ref dataFrameCount);

            if (count >= request.MinimumFrames)
                Complete(CompletionReason.MinimumFramesReached);
        };

        parser.ParsingException += (_, _) => Interlocked.Increment(ref parsingExceptionCount);

        parser.ExceededParsingExceptionThreshold += (_, _) => Complete(CompletionReason.ParsingExceptionThresholdExceeded);

        parser.ConnectionException += (_, e) =>
        {
            lastError = e.Argument1;
            Complete(CompletionReason.ConnectionError);
        };

        parser.ConnectionTerminated += (_, _) =>
        {
            // For file playback with AutoRepeat disabled, the stream ends on its own - if that happens
            // before the minimum frame count is reached, fail fast rather than waiting for the timeout.
            if (request.TransportProtocol == TransportProtocol.File && !request.AutoRepeat)
                Complete(CompletionReason.PlaybackEnded);
        };

        parser.PhasorProtocol = request.PhasorProtocol;
        parser.TransportProtocol = request.TransportProtocol;
        parser.ConnectionString = request.ConnectionString;
        parser.DeviceID = request.DeviceIdCode;
        parser.DefinedFrameRate = request.FrameRate;
        parser.AutoRepeatCapturedPlayback = request.AutoRepeat;
        parser.MaximumConnectionAttempts = 1;
        parser.AllowedParsingExceptions = 10;
        parser.ParsingExceptionWindow = Ticks.FromSeconds(5.0D);

        try
        {
            parser.Start();

            bool signaled = completionGate.Wait(TimeSpan.FromSeconds(request.TimeoutSeconds));

            if (!signaled)
                completion = CompletionReason.Timeout;
        }
        catch (Exception ex)
        {
            lastError = ex;
            completion = CompletionReason.ConnectionError;
        }
        finally
        {
            parser.Stop();
        }

        stopwatch.Stop();

        return new PmuTestOutcome
        {
            Completion = completion,
            ConfigurationFrameReceived = configurationFrameReceived,
            ConfigurationFrame = configurationFrame,
            TotalFramesReceived = parser.TotalFramesReceived,
            DataFramesReceived = dataFrameCount,
            TotalMissingFrames = parser.TotalMissingFrames,
            TotalCrcExceptions = parser.TotalCrcExceptions,
            ParsingExceptions = parsingExceptionCount,
            CalculatedFrameRate = parser.CalculatedFrameRate,
            IsConnected = parser.IsConnected,
            MinimumFramesRequired = request.MinimumFrames,
            ElapsedMilliseconds = stopwatch.ElapsedMilliseconds,
            LastError = lastError
        };
    }
}
