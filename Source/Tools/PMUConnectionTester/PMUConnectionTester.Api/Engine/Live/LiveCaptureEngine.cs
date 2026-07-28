using ConnectionTester.Api.Models;
using GSF.PhasorProtocols;
using GSF.Units.EE;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Headless orchestration of <see cref="MultiProtocolFrameParser"/> for a live capture session.
/// Drives the Conectando -&gt; AguardandoComunicacao -&gt; CapturandoDados -&gt; EncerrandoCaptura
/// -&gt; Concluido state machine, mutating <see cref="LiveSession"/> as it progresses. Runs on its
/// own dedicated thread (started by <see cref="LiveSessionOrchestrator"/> via <c>Task.Run</c>) for
/// the life of the session.
/// </summary>
internal class LiveCaptureEngine : ILiveCaptureEngine
{
    public void Run(LiveSession session, LiveCaptureRequest request)
    {
        using MultiProtocolFrameParser parser = new();
        session.Parser = parser;

        CancellationToken cancellationToken = session.CancellationSource.Token;
        ManualResetEventSlim phaseGate = new(false);
        int terminalFlag = 0;
        bool connectionEstablished = false;
        bool communicationReceived = false;
        bool capturing = false;

        void Fail(LiveSessionStep step, string message)
        {
            if (Interlocked.CompareExchange(ref terminalFlag, 1, 0) == 0)
                session.Fail(step, message);

            phaseGate.Set();
        }

        parser.ConnectionEstablished += (_, _) =>
        {
            connectionEstablished = true;
            phaseGate.Set();
        };

        parser.ConnectionException += (_, e) =>
        {
            if (!connectionEstablished)
                Fail(LiveSessionStep.Conectando, $"Falha ao conectar no dispositivo: {e.Argument1?.Message ?? "erro desconhecido"}.");
            else
                Fail(LiveSessionStep.CapturandoDados, "Conexão perdida durante a captura.");
        };

        parser.ConnectionTerminated += (_, _) =>
        {
            if (capturing)
                Fail(LiveSessionStep.CapturandoDados, "Conexão perdida durante a captura.");
        };

        parser.ReceivedConfigurationFrame += (_, e) =>
        {
            session.SetConfiguration(LiveDtoMapper.BuildConfiguration(e.Argument));

            if (!communicationReceived)
            {
                communicationReceived = true;
                phaseGate.Set();
            }
        };

        parser.ReceivedDataFrame += (_, e) =>
        {
            if (!communicationReceived)
            {
                communicationReceived = true;
                phaseGate.Set();
            }

            if (capturing)
                RecordDataFrame(session, e.Argument);
        };

        parser.PhasorProtocol = request.PhasorProtocol;
        parser.TransportProtocol = request.TransportProtocol;
        parser.ConnectionString = request.ConnectionString;
        parser.DeviceID = request.DeviceIdCode;
        parser.DefinedFrameRate = request.FrameRate;
        parser.AutoRepeatCapturedPlayback = request.AutoRepeat;
        parser.MaximumConnectionAttempts = 1;

        try
        {
            // Phase 1: Conectando - wait for the socket/handshake-level connection to complete.
            session.AdvanceStep(LiveSessionStep.Conectando);
            parser.Start();

            bool signaled = phaseGate.Wait(TimeSpan.FromSeconds(request.MinCommunicationWaitSeconds), cancellationToken);

            if (Volatile.Read(ref terminalFlag) == 1)
                return;

            if (!signaled)
            {
                Fail(LiveSessionStep.Conectando, "Falha ao conectar no dispositivo: tempo excedido.");
                return;
            }

            // Phase 2: AguardandoComunicacao - connected, now wait for the first frame of any kind.
            phaseGate.Reset();
            session.AdvanceStep(LiveSessionStep.AguardandoComunicacao);

            signaled = phaseGate.Wait(TimeSpan.FromSeconds(request.MinCommunicationWaitSeconds), cancellationToken);

            if (Volatile.Read(ref terminalFlag) == 1)
                return;

            if (!signaled)
            {
                Fail(LiveSessionStep.AguardandoComunicacao, $"Nenhuma comunicação recebida em {request.MinCommunicationWaitSeconds}s.");
                return;
            }

            // Phase 3: CapturandoDados - accumulate measurements for the capture window.
            phaseGate.Reset();
            DateTime captureStartTime = DateTime.UtcNow;
            capturing = true;
            session.AdvanceStep(LiveSessionStep.CapturandoDados);

            phaseGate.Wait(TimeSpan.FromSeconds(request.CaptureDurationSeconds), cancellationToken);
            capturing = false;

            if (Volatile.Read(ref terminalFlag) == 1)
                return;

            DateTime captureEndTime = DateTime.UtcNow;

            // Phase 4: EncerrandoCaptura
            session.AdvanceStep(LiveSessionStep.EncerrandoCaptura);

            try
            {
                parser.Stop();
            }
            catch (Exception ex)
            {
                Fail(LiveSessionStep.EncerrandoCaptura, $"Falha ao encerrar a captura: {ex.Message}.");
                return;
            }

            session.Complete(captureStartTime, captureEndTime, request.CaptureDurationSeconds);
        }
        catch (OperationCanceledException)
        {
            // Session was cancelled (DELETE) - no error state to record, just clean up below.
        }
        finally
        {
            try { parser.Stop(); } catch { /* already stopping/stopped */ }
        }
    }

    private static void RecordDataFrame(LiveSession session, IDataFrame frame)
    {
        DateTime timestamp = frame.Timestamp;

        // Stamped here, synchronously, as the frame is handed off by the parser - this is the
        // closest this process gets to "when the frame actually arrived", used by the consumer to
        // compute real per-frame latency instead of comparing every sample against a single "now"
        // taken after the whole capture window ends.
        DateTime receivedAtUtc = DateTime.UtcNow;

        foreach (IDataCell cell in frame.Cells)
        {
            double frequency = cell.FrequencyValue?.Frequency ?? 0.0D;
            double rocof = cell.FrequencyValue?.DfDt ?? 0.0D;

            List<PmuPhasorMeasurementDto> phasors = new();
            IPhasorValue firstVoltage = null;
            IPhasorValue firstCurrent = null;

            if (cell.PhasorValues is not null)
            {
                foreach (IPhasorValue phasor in cell.PhasorValues)
                {
                    if (phasor?.Definition is null)
                        continue;

                    bool isVoltage = phasor.Definition.PhasorType == PhasorType.Voltage;

                    phasors.Add(new PmuPhasorMeasurementDto
                    {
                        Label = phasor.Definition.Label,
                        Type = isVoltage ? "FV" : "FA",
                        Magnitude = phasor.AdjustedMagnitude(),
                        Angle = phasor.AdjustedAngle().ToDegrees()
                    });

                    if (isVoltage)
                        firstVoltage ??= phasor;
                    else
                        firstCurrent ??= phasor;
                }
            }

            double? activePower = null;
            double? reactivePower = null;

            if (firstVoltage is not null && firstCurrent is not null)
            {
                activePower = PhasorValueBase.CalculatePower(firstVoltage, firstCurrent) / 1000000.0D;
                reactivePower = PhasorValueBase.CalculateVars(firstVoltage, firstCurrent) / 1000000.0D;
            }

            session.AppendMeasurement(cell.IDCode, new PmuMeasurementSampleDto
            {
                Timestamp = timestamp,
                ReceivedAtUtc = receivedAtUtc,
                Frequency = frequency,
                Rocof = rocof,
                ActivePower = activePower,
                ReactivePower = reactivePower,
                Phasors = phasors,
                Stat = cell.StatusFlags
            });
        }
    }
}