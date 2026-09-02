using ConnectionTester.Api.Controllers;
using ConnectionTester.Api.Engine.Live;
using ConnectionTester.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace PMUConnectionTester.Api.Tests.Controllers;

/// <summary>
/// Drives <see cref="LiveConnectionTesterController"/> against a mocked <see
/// cref="ILiveSessionOrchestrator"/> so every status-code branch can be exercised
/// deterministically, without a real network connection.
/// </summary>
[TestClass]
public class LiveConnectionTesterControllerTests
{
    [TestMethod]
    public void GetSessionConfiguration_NotFound_ReturnsNotFound()
    {
        Guid sessionId = Guid.NewGuid();
        PmuConfigurationDto configuration = null;
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.TryGetConfiguration(sessionId, out configuration)).Returns(LiveLookupResult.NotFound);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        NegotiatedContentResult<string> result = controller.GetSessionConfiguration(sessionId) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
    }

    [TestMethod]
    public void GetSessionConfiguration_NotReady_ReturnsConflict()
    {
        Guid sessionId = Guid.NewGuid();
        PmuConfigurationDto configuration = null;
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.TryGetConfiguration(sessionId, out configuration)).Returns(LiveLookupResult.NotReady);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        NegotiatedContentResult<string> result = controller.GetSessionConfiguration(sessionId) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);
    }

    [TestMethod]
    public void GetSessionConfiguration_Ready_ReturnsOk()
    {
        Guid sessionId = Guid.NewGuid();
        PmuConfigurationDto configuration = new() { Result = "Success", FrameRate = 58 };
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.TryGetConfiguration(sessionId, out configuration)).Returns(LiveLookupResult.Ok);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        OkNegotiatedContentResult<PmuConfigurationDto> result = controller.GetSessionConfiguration(sessionId) as OkNegotiatedContentResult<PmuConfigurationDto>;

        Assert.IsNotNull(result);
        Assert.AreEqual(58, result.Content.FrameRate);
    }

    [TestMethod]
    public void GetSessionMeasurements_NotReady_ReturnsConflict()
    {
        Guid sessionId = Guid.NewGuid();
        List<PmuMeasurementSeriesDto> measurements = null;
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.TryGetMeasurements(sessionId, out measurements)).Returns(LiveLookupResult.NotReady);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        NegotiatedContentResult<string> result = controller.GetSessionMeasurements(sessionId) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);
    }

    [TestMethod]
    public void GetSessionMeasurements_Ready_ReturnsOk()
    {
        Guid sessionId = Guid.NewGuid();
        List<PmuMeasurementSeriesDto> measurements = new() { new PmuMeasurementSeriesDto { IdPmu = "10105" } };
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.TryGetMeasurements(sessionId, out measurements)).Returns(LiveLookupResult.Ok);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        OkNegotiatedContentResult<List<PmuMeasurementSeriesDto>> result = controller.GetSessionMeasurements(sessionId) as OkNegotiatedContentResult<List<PmuMeasurementSeriesDto>>;

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Content.Count);
    }

    [TestMethod]
    public void GetSessionStatus_Found_ReturnsOk()
    {
        Guid sessionId = Guid.NewGuid();
        LiveSessionStatusDto statusDto = new() { SessionId = sessionId, Step = LiveSessionStep.CapturandoDados, Status = LiveSessionStatus.EmAndamento };
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.TryGetStatus(sessionId, out statusDto)).Returns(LiveLookupResult.Ok);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        OkNegotiatedContentResult<LiveSessionStatusDto> result = controller.GetSessionStatus(sessionId) as OkNegotiatedContentResult<LiveSessionStatusDto>;

        Assert.IsNotNull(result);
        Assert.AreEqual(sessionId, result.Content.SessionId);
    }

    [TestMethod]
    public void GetSessionStatus_NotFound_ReturnsNotFound()
    {
        Guid sessionId = Guid.NewGuid();
        LiveSessionStatusDto statusDto = null;
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.TryGetStatus(sessionId, out statusDto)).Returns(LiveLookupResult.NotFound);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        NegotiatedContentResult<string> result = controller.GetSessionStatus(sessionId) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
    }

    [TestMethod]
    public void StartSession_InvalidModelState_ReturnsBadRequest()
    {
        LiveConnectionTesterController controller = new(Mock.Of<ILiveSessionOrchestrator>());
        controller.ModelState.AddModelError(nameof(StartLiveSessionRequest.Host), "Required");

        NegotiatedContentResult<string> result = controller.StartSession(new StartLiveSessionRequest()) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [TestMethod]
    public void StartSession_InvalidPhasorProtocol_ReturnsBadRequest()
    {
        LiveConnectionTesterController controller = new(Mock.Of<ILiveSessionOrchestrator>());
        StartLiveSessionRequest request = ValidRequest();
        request.PhasorProtocol = "NotAProtocol";

        NegotiatedContentResult<string> result = controller.StartSession(request) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        StringAssert.Contains(result.Content, "NotAProtocol");
    }

    [TestMethod]
    public void StartSession_NullRequest_ReturnsBadRequest()
    {
        LiveConnectionTesterController controller = new(Mock.Of<ILiveSessionOrchestrator>());

        NegotiatedContentResult<string> result = controller.StartSession(null) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [TestMethod]
    public void StartSession_OrchestratorThrows_ReturnsServiceUnavailable()
    {
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.Start(It.IsAny<LiveCaptureRequest>())).Throws(new InvalidOperationException("no threads available"));

        LiveConnectionTesterController controller = new(orchestrator.Object);

        NegotiatedContentResult<string> result = controller.StartSession(ValidRequest()) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.ServiceUnavailable, result.StatusCode);
    }

    [TestMethod]
    public void StartSession_UnsupportedTransportProtocol_ReturnsBadRequest()
    {
        LiveConnectionTesterController controller = new(Mock.Of<ILiveSessionOrchestrator>());
        StartLiveSessionRequest request = ValidRequest();
        request.TransportProtocol = "File";

        NegotiatedContentResult<string> result = controller.StartSession(request) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        StringAssert.Contains(result.Content, "File");
    }

    [TestMethod]
    public void StartSession_ValidRequest_ReturnsCreatedWithSessionDto()
    {
        LiveSessionDto sessionDto = new() { SessionId = Guid.NewGuid(), Step = LiveSessionStep.Conectando, Status = LiveSessionStatus.EmAndamento, CreatedAt = DateTime.UtcNow };
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.Start(It.IsAny<LiveCaptureRequest>())).Returns(sessionDto);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        NegotiatedContentResult<LiveSessionDto> result = controller.StartSession(ValidRequest()) as NegotiatedContentResult<LiveSessionDto>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        Assert.AreEqual(sessionDto.SessionId, result.Content.SessionId);
    }

    [TestMethod]
    public void StopSession_Existing_ReturnsNoContent()
    {
        Guid sessionId = Guid.NewGuid();
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.Stop(sessionId)).Returns(true);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        StatusCodeResult result = controller.StopSession(sessionId) as StatusCodeResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
    }

    [TestMethod]
    public void StopSession_NeverExisted_ReturnsNotFound()
    {
        Guid sessionId = Guid.NewGuid();
        Mock<ILiveSessionOrchestrator> orchestrator = new();
        orchestrator.Setup(o => o.Stop(sessionId)).Returns(false);

        LiveConnectionTesterController controller = new(orchestrator.Object);

        NegotiatedContentResult<string> result = controller.StopSession(sessionId) as NegotiatedContentResult<string>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
    }

    private static StartLiveSessionRequest ValidRequest() => new()
    {
        TransportProtocol = "Tcp",
        Host = "10.20.30.40",
        Port = 4712,
        DeviceIdCode = 10105,
        PhasorProtocol = "IEEEC37_118V2"
    };
}