using System;
using System.IO;
using ConnectionTester.Api.Controllers;
using ConnectionTester.Api.Engine;
using ConnectionTester.Api.Infrastructure;
using ConnectionTester.Api.Models;
using GSF.Communication;
using GSF.PhasorProtocols;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace PMUConnectionTester.Api.Tests.Controllers;

/// <summary>
/// Drives <see cref="PmuConnectionTesterController"/> against a mocked <see cref="IPmuConnectionTestEngine"/>
/// so every branch of the outcome-to-response mapping, and the per-endpoint request-building logic, can be
/// exercised deterministically - without a real network connection or file playback.
/// </summary>
[TestClass]
public class PmuConnectionTesterControllerMoqTests
{
    private static readonly string SampleCapturePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Sample1344.PmuCapture");

    private static PmuTestOutcome Outcome(
        CompletionReason completion,
        int dataFrames = 10,
        int parsingExceptions = 0,
        int minimumFramesRequired = 10,
        Exception lastError = null,
        bool isConnected = true) =>
        new()
        {
            Completion = completion,
            ConfigurationFrameReceived = false,
            ConfigurationFrame = null,
            TotalFramesReceived = dataFrames,
            DataFramesReceived = dataFrames,
            TotalMissingFrames = 0,
            TotalCrcExceptions = 0,
            ParsingExceptions = parsingExceptions,
            CalculatedFrameRate = 30,
            IsConnected = isConnected,
            MinimumFramesRequired = minimumFramesRequired,
            ElapsedMilliseconds = 123,
            LastError = lastError
        };

    private static TcpTestRequest ValidTcpRequest() => new()
    {
        Host = "10.0.0.5",
        Port = 4712,
        Protocol = "IEEE1344",
        DeviceIdCode = 42,
        FrameRate = 30
    };

    private static UdpTestRequest ValidUdpRequest() => new()
    {
        Host = "10.0.0.5",
        LocalPort = 4712,
        RemotePort = 4713,
        Protocol = "IEEE1344",
        DeviceIdCode = 42,
        FrameRate = 30
    };

    private static FileTestRequest ValidFileRequest() => new()
    {
        Filename = SampleCapturePath,
        Protocol = "IEEE1344",
        DeviceIdCode = 42,
        FrameRate = 30
    };

    [TestMethod]
    public void Tcp_MinimumFramesReachedWithNoParsingExceptions_ReturnsPass()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.MinimumFramesReached, dataFrames: 15, parsingExceptions: 0));

        PmuConnectionTesterController controller = new(engine.Object);
        OkNegotiatedContentResult<TestResponse> result = controller.Tcp(ValidTcpRequest()) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("PASS", result.Content.OverallStatus);
        StringAssert.Contains(result.Content.Message, "15 frames received");
        engine.Verify(e => e.Run(It.IsAny<PmuTestRequest>()), Times.Once);
    }

    [TestMethod]
    public void Tcp_MinimumFramesReachedWithParsingExceptions_ReturnsFail()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.MinimumFramesReached, parsingExceptions: 3));

        PmuConnectionTesterController controller = new(engine.Object);
        OkNegotiatedContentResult<TestResponse> result = controller.Tcp(ValidTcpRequest()) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("FAIL", result.Content.OverallStatus);
        StringAssert.Contains(result.Content.Message, "3 parsing exception(s)");
    }

    [TestMethod]
    public void Tcp_ParsingExceptionThresholdExceeded_ReturnsFail()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.ParsingExceptionThresholdExceeded));

        PmuConnectionTesterController controller = new(engine.Object);
        OkNegotiatedContentResult<TestResponse> result = controller.Tcp(ValidTcpRequest()) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("FAIL", result.Content.OverallStatus);
        StringAssert.Contains(result.Content.Message, "Exceeded the allowed parsing exception threshold");
    }

    [TestMethod]
    public void Tcp_ConnectionErrorWithMessage_ReturnsFailIncludingErrorMessage()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.ConnectionError, lastError: new InvalidOperationException("host unreachable")));

        PmuConnectionTesterController controller = new(engine.Object);
        OkNegotiatedContentResult<TestResponse> result = controller.Tcp(ValidTcpRequest()) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("FAIL", result.Content.OverallStatus);
        StringAssert.Contains(result.Content.Message, "host unreachable");
    }

    [TestMethod]
    public void Tcp_ConnectionErrorWithoutMessage_ReturnsFailWithUnknownError()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.ConnectionError, lastError: null));

        PmuConnectionTesterController controller = new(engine.Object);
        OkNegotiatedContentResult<TestResponse> result = controller.Tcp(ValidTcpRequest()) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("FAIL", result.Content.OverallStatus);
        StringAssert.Contains(result.Content.Message, "unknown error");
    }

    [TestMethod]
    public void File_PlaybackEndedBeforeMinimumFrames_ReturnsFailMentioningFrameCounts()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.PlaybackEnded, dataFrames: 4, minimumFramesRequired: 10));

        PmuConnectionTesterController controller = new(engine.Object);
        OkNegotiatedContentResult<TestResponse> result = controller.File(ValidFileRequest()) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("FAIL", result.Content.OverallStatus);
        StringAssert.Contains(result.Content.Message, "before the minimum frame count (10)");
        StringAssert.Contains(result.Content.Message, "received 4 frame(s)");
    }

    [TestMethod]
    public void File_TimeoutExceeded_ReturnsFailMentioningFrameCounts()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.Timeout, dataFrames: 7, minimumFramesRequired: 20));

        PmuConnectionTesterController controller = new(engine.Object);
        OkNegotiatedContentResult<TestResponse> result = controller.File(ValidFileRequest()) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("FAIL", result.Content.OverallStatus);
        StringAssert.Contains(result.Content.Message, "7 of 20 required frames");
    }

    [TestMethod]
    public void Tcp_MapsRequestFieldsAndBuildsExpectedConnectionString()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        PmuTestRequest captured = null;

        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Callback<PmuTestRequest>(r => captured = r)
              .Returns(Outcome(CompletionReason.MinimumFramesReached));

        PmuConnectionTesterController controller = new(engine.Object);
        TcpTestRequest request = new()
        {
            Host = "10.0.0.5",
            Port = 4712,
            Protocol = "IEEE1344",
            DeviceIdCode = 7,
            FrameRate = 60,
            IsListener = true,
            MinimumFrames = 5,
            TimeoutSeconds = 12
        };

        controller.Tcp(request);

        Assert.IsNotNull(captured);
        Assert.AreEqual(TransportProtocol.Tcp, captured.TransportProtocol);
        Assert.AreEqual(PhasorProtocol.IEEE1344, captured.PhasorProtocol);
        Assert.AreEqual("server=10.0.0.5; port=4712; interface=0.0.0.0; islistener=True", captured.ConnectionString);
        Assert.AreEqual((ushort)7, captured.DeviceIdCode);
        Assert.AreEqual(60, captured.FrameRate);
        Assert.IsFalse(captured.AutoRepeat);
        Assert.AreEqual(5, captured.MinimumFrames);
        Assert.AreEqual(12, captured.TimeoutSeconds);
    }

    [TestMethod]
    public void Udp_MapsRequestFieldsAndBuildsExpectedConnectionString()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        PmuTestRequest captured = null;

        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Callback<PmuTestRequest>(r => captured = r)
              .Returns(Outcome(CompletionReason.MinimumFramesReached));

        PmuConnectionTesterController controller = new(engine.Object);
        UdpTestRequest request = new()
        {
            Host = "10.0.0.5",
            LocalPort = 4712,
            RemotePort = 4713,
            Protocol = "IEEE1344",
            DeviceIdCode = 9,
            FrameRate = 60,
            MinimumFrames = 6,
            TimeoutSeconds = 8
        };

        controller.Udp(request);

        Assert.IsNotNull(captured);
        Assert.AreEqual(TransportProtocol.Udp, captured.TransportProtocol);
        Assert.AreEqual(PhasorProtocol.IEEE1344, captured.PhasorProtocol);
        Assert.AreEqual("localport=4712; server=10.0.0.5; remoteport=4713; interface=0.0.0.0", captured.ConnectionString);
        Assert.AreEqual((ushort)9, captured.DeviceIdCode);
        Assert.IsFalse(captured.AutoRepeat);
        Assert.AreEqual(6, captured.MinimumFrames);
        Assert.AreEqual(8, captured.TimeoutSeconds);
    }

    [TestMethod]
    public void File_NoOverridesProvided_ResolvesConfiguredDefaultsAndBuildsExpectedConnectionString()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        PmuTestRequest captured = null;

        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Callback<PmuTestRequest>(r => captured = r)
              .Returns(Outcome(CompletionReason.MinimumFramesReached));

        PmuConnectionTesterController controller = new(engine.Object);
        FileTestRequest request = ValidFileRequest();

        controller.File(request);

        Assert.IsNotNull(captured);
        Assert.AreEqual(TransportProtocol.File, captured.TransportProtocol);
        Assert.AreEqual($"file={SampleCapturePath}", captured.ConnectionString);
        Assert.IsTrue(captured.AutoRepeat);
        Assert.AreEqual(ApiSettings.DefaultMinimumFrames, captured.MinimumFrames);
        Assert.AreEqual(ApiSettings.MaxExecutionTimeoutSeconds, captured.TimeoutSeconds);
    }

    [TestMethod]
    public void Tcp_EngineIsInvokedExactlyOnce()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.MinimumFramesReached));

        PmuConnectionTesterController controller = new(engine.Object);
        controller.Tcp(ValidTcpRequest());

        engine.Verify(e => e.Run(It.IsAny<PmuTestRequest>()), Times.Once);
    }

    [TestMethod]
    public void Tcp_InvalidProtocol_NeverInvokesEngine()
    {
        Mock<IPmuConnectionTestEngine> engine = new();
        engine.Setup(e => e.Run(It.IsAny<PmuTestRequest>()))
              .Returns(Outcome(CompletionReason.MinimumFramesReached));

        PmuConnectionTesterController controller = new(engine.Object);
        TcpTestRequest request = ValidTcpRequest();
        request.Protocol = "NotAProtocol";

        controller.Tcp(request);

        engine.Verify(e => e.Run(It.IsAny<PmuTestRequest>()), Times.Never);
    }
}
