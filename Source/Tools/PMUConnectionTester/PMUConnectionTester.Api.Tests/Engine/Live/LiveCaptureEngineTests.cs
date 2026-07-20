using ConnectionTester.Api.Engine.Live;
using ConnectionTester.Api.Models;
using GSF.Communication;
using GSF.PhasorProtocols;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace PMUConnectionTester.Api.Tests.Engine.Live;

/// <summary>
/// Drives the real <see cref="LiveCaptureEngine"/> state machine against the existing sample
/// capture file (via <see cref="TransportProtocol.File"/>, bypassing the public controller's
/// Tcp/Udp-only restriction - the engine itself is transport-agnostic). This is the only practical
/// way to exercise the frame-parsing/DTO-building logic without live PMU hardware.
/// </summary>
[TestClass]
public class LiveCaptureEngineTests
{
    private static readonly string SampleCapturePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Sample1344.PmuCapture");

    [TestMethod]
    public void Run_Cancelled_StopsWithoutThrowing()
    {
        LiveSession session = new();
        LiveCaptureEngine engine = new();

        session.Cancel();
        engine.Run(session, FileRequest());

        // No assertion beyond "didn't throw" - cancellation before Run() starts should exit cleanly.
    }

    [TestMethod]
    public void Run_UnresolvableHost_FailsAtConectandoStep()
    {
        LiveCaptureRequest request = new()
        {
            PhasorProtocol = PhasorProtocol.IEEEC37_118V2,
            TransportProtocol = TransportProtocol.Tcp,
            ConnectionString = "server=127.0.0.1; port=1; interface=0.0.0.0; islistener=false",
            DeviceIdCode = 1,
            FrameRate = 30,
            MinCommunicationWaitSeconds = 3,
            CaptureDurationSeconds = 3
        };

        LiveSession session = new();
        LiveCaptureEngine engine = new();

        engine.Run(session, request);

        LiveSessionStatusDto status = session.ToStatusDto();
        Assert.AreEqual(LiveSessionStatus.Erro, status.Status);
        Assert.AreEqual(LiveSessionStep.Conectando, status.Step);
        StringAssert.Contains(status.ErrorMessage, "Falha ao conectar");
    }

    [TestMethod]
    public void Run_ValidSampleCapture_ReachesConcluidoWithRealMeasurements()
    {
        Assert.IsTrue(System.IO.File.Exists(SampleCapturePath), $"Test capture file not found at {SampleCapturePath}");

        LiveSession session = new();
        LiveCaptureEngine engine = new();

        engine.Run(session, FileRequest());

        LiveSessionStatusDto status = session.ToStatusDto();
        Assert.AreEqual(LiveSessionStatus.Concluida, status.Status, status.ErrorMessage);
        Assert.AreEqual(LiveSessionStep.Concluido, status.Step);

        Assert.IsTrue(session.TryGetConfiguration(out PmuConfigurationDto configuration));
        Assert.AreEqual("Success", configuration.Result);
        Assert.IsTrue(configuration.Cells.Count > 0);
        Assert.IsFalse(string.IsNullOrWhiteSpace(configuration.Cells[0].StationName));
        Assert.IsTrue(configuration.Cells[0].Phasors.Count > 0);

        Assert.IsTrue(session.TryGetMeasurements(out var series));
        Assert.IsTrue(series.Count > 0);

        PmuMeasurementSeriesDto deviceSeries = series[0];
        Assert.IsTrue(deviceSeries.Measurements.Count > 0);
        Assert.AreEqual(deviceSeries.Measurements.Count, deviceSeries.ReceivedFrameCount);

        PmuMeasurementSampleDto sample = deviceSeries.Measurements[0];
        Assert.AreNotEqual(0.0D, sample.Frequency);
        Assert.IsTrue(sample.Phasors.Count > 0);
        Assert.IsTrue(sample.Phasors.All(p => p.Type == "FV" || p.Type == "FA"));
    }

    private static LiveCaptureRequest FileRequest(int minCommunicationWaitSeconds = 10, int captureDurationSeconds = 3) => new()
    {
        PhasorProtocol = PhasorProtocol.IEEE1344,
        TransportProtocol = TransportProtocol.File,
        ConnectionString = $"file={SampleCapturePath}",
        DeviceIdCode = 0,
        FrameRate = 30,
        AutoRepeat = true,
        MinCommunicationWaitSeconds = minCommunicationWaitSeconds,
        CaptureDurationSeconds = captureDurationSeconds
    };
}