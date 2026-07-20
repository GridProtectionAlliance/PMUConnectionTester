using ConnectionTester.Api.Models;
using GSF.PhasorProtocols;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Mutable state for one live capture session. Written to by the background capture thread ( <see
/// cref="LiveCaptureEngine"/>) and read concurrently by polling HTTP requests - every access goes
/// through <see cref="_gate"/> since writes (per-frame measurement samples) can be frequent.
/// </summary>
internal class LiveSession
{
    private readonly object _gate = new();
    private readonly Dictionary<ushort, List<PmuMeasurementSampleDto>> _samplesByDevice = new();

    public CancellationTokenSource CancellationSource { get; } = new();
    public PmuConfigurationDto Configuration { get; private set; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public string ErrorMessage { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public List<PmuMeasurementSeriesDto> MeasurementSeries { get; private set; }

    /// <summary>
    /// Parser driving this session's capture - held so <see cref="Cancel"/> can stop it immediately.
    /// </summary>
    public MultiProtocolFrameParser Parser { get; set; }

    public Guid SessionId { get; } = Guid.NewGuid();

    public LiveSessionStatus Status { get; private set; } = LiveSessionStatus.EmAndamento;
    public LiveSessionStep Step { get; private set; } = LiveSessionStep.Conectando;

    public void AdvanceStep(LiveSessionStep step)
    {
        lock (_gate)
            Step = step;
    }

    public void AppendMeasurement(ushort deviceIdCode, PmuMeasurementSampleDto sample)
    {
        lock (_gate)
        {
            if (!_samplesByDevice.TryGetValue(deviceIdCode, out List<PmuMeasurementSampleDto> samples))
            {
                samples = new List<PmuMeasurementSampleDto>();
                _samplesByDevice[deviceIdCode] = samples;
            }

            samples.Add(sample);
        }
    }

    public void Cancel()
    {
        CancellationSource.Cancel();

        lock (_gate)
        {
            // Mark the session finished immediately, even though the background thread may still be
            // unwinding - otherwise a cancelled-before-connecting session would never have
            // FinishedAt set and would never become eligible for the retention sweep.
            if (!FinishedAt.HasValue)
            {
                Status = LiveSessionStatus.Erro;
                ErrorMessage = "Sessão cancelada.";
                FinishedAt = DateTime.UtcNow;
            }
        }

        try
        {
            Parser?.Stop();
        }
        catch
        {
            // Best-effort - the session is being torn down regardless.
        }
    }

    public void Complete(DateTime captureStartTime, DateTime captureEndTime, int captureDurationSeconds)
    {
        lock (_gate)
        {
            MeasurementSeries = new List<PmuMeasurementSeriesDto>();

            foreach (KeyValuePair<ushort, List<PmuMeasurementSampleDto>> entry in _samplesByDevice)
            {
                MeasurementSeries.Add(new PmuMeasurementSeriesDto
                {
                    IdPmu = entry.Key.ToString(),
                    ReceivedFrameCount = entry.Value.Count,
                    FpsWindowSeconds = captureDurationSeconds,
                    CaptureStartTime = captureStartTime,
                    CaptureEndTime = captureEndTime,
                    Measurements = entry.Value
                });
            }

            Step = LiveSessionStep.Concluido;
            Status = LiveSessionStatus.Concluida;
            FinishedAt = DateTime.UtcNow;
        }
    }

    public void Fail(LiveSessionStep step, string errorMessage)
    {
        lock (_gate)
        {
            Step = step;
            Status = LiveSessionStatus.Erro;
            ErrorMessage = errorMessage;
            FinishedAt = DateTime.UtcNow;
        }
    }

    public bool IsFinished()
    {
        lock (_gate)
            return FinishedAt.HasValue;
    }

    public void SetConfiguration(PmuConfigurationDto configuration)
    {
        lock (_gate)
            Configuration = configuration;
    }

    public LiveSessionStatusDto ToStatusDto()
    {
        lock (_gate)
        {
            return new LiveSessionStatusDto
            {
                SessionId = SessionId,
                Step = Step,
                Status = Status,
                ErrorMessage = ErrorMessage,
                StartedAt = CreatedAt,
                FinishedAt = FinishedAt
            };
        }
    }

    public bool TryGetConfiguration(out PmuConfigurationDto configuration)
    {
        lock (_gate)
        {
            configuration = Configuration;
            return configuration is not null;
        }
    }

    public bool TryGetMeasurements(out List<PmuMeasurementSeriesDto> series)
    {
        lock (_gate)
        {
            series = MeasurementSeries;
            return Status == LiveSessionStatus.Concluida && series is not null;
        }
    }
}