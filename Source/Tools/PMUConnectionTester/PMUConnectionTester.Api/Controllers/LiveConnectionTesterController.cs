using ConnectionTester.Api.Engine.Live;
using ConnectionTester.Api.Infrastructure;
using ConnectionTester.Api.Models;
using GSF.Communication;
using GSF.PhasorProtocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace ConnectionTester.Api.Controllers;

/// <summary>
/// Session-plus-polling endpoints for real ("live") PMU/PDC connectivity captures. Unlike <see
/// cref="PmuConnectionTesterController"/>, these endpoints return immediately - the capture itself
/// (60-180+ seconds) runs in the background, and callers poll <c>GET .../sessions/{sessionId}</c>
/// for progress.
/// </summary>
[RoutePrefix("api/pmuconnectiontester/live")]
public class LiveConnectionTesterController : ApiController
{
    private readonly ILiveSessionOrchestrator _orchestrator;

    public LiveConnectionTesterController() : this(new LiveSessionOrchestrator())
    {
    }

    internal LiveConnectionTesterController(ILiveSessionOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    /// <summary>
    /// Retrieves the configuration (CFG) frame received from the device during this session.
    /// </summary>
    /// <param name="sessionId">Identifier returned by <see cref="StartSession"/>.</param>
    /// <response code="200">The configuration frame was received and is returned below.</response>
    /// <response code="404">The session does not exist, or has expired/been cleaned up.</response>
    /// <response code="409">The session hasn't received a configuration frame yet.</response>
    [HttpGet]
    [Route("sessions/{sessionId}/configuration")]
    [ResponseType(typeof(PmuConfigurationDto))]
    public IHttpActionResult GetSessionConfiguration(Guid sessionId)
    {
        return _orchestrator.TryGetConfiguration(sessionId, out PmuConfigurationDto configuration) switch
        {
            LiveLookupResult.Ok => Ok(configuration),
            LiveLookupResult.NotReady => Content(HttpStatusCode.Conflict, $"Session \"{sessionId}\" has not received a configuration frame yet."),
            _ => Content(HttpStatusCode.NotFound, $"Session \"{sessionId}\" was not found.")
        };
    }

    /// <summary>
    /// Retrieves the measurement series captured during this session.
    /// </summary>
    /// <param name="sessionId">Identifier returned by <see cref="StartSession"/>.</param>
    /// <response code="200">
    /// The session finished successfully; the captured series are returned below.
    /// </response>
    /// <response code="404">The session does not exist, or has expired/been cleaned up.</response>
    /// <response code="409">
    /// The session is still in progress (or ended in error), so no measurements are available.
    /// </response>
    [HttpGet]
    [Route("sessions/{sessionId}/measurements")]
    [ResponseType(typeof(List<PmuMeasurementSeriesDto>))]
    public IHttpActionResult GetSessionMeasurements(Guid sessionId)
    {
        return _orchestrator.TryGetMeasurements(sessionId, out List<PmuMeasurementSeriesDto> measurements) switch
        {
            LiveLookupResult.Ok => Ok(measurements),
            LiveLookupResult.NotReady => Content(HttpStatusCode.Conflict, $"Session \"{sessionId}\" has not finished capturing yet."),
            _ => Content(HttpStatusCode.NotFound, $"Session \"{sessionId}\" was not found.")
        };
    }

    /// <summary>
    /// Queries the progress of a live capture session.
    /// </summary>
    /// <param name="sessionId">Identifier returned by <see cref="StartSession"/>.</param>
    /// <response code="200">
    /// The session was found; check <see cref="LiveSessionStatusDto.Status"/> for progress.
    /// </response>
    /// <response code="404">The session does not exist, or has expired/been cleaned up.</response>
    [HttpGet]
    [Route("sessions/{sessionId}")]
    [ResponseType(typeof(LiveSessionStatusDto))]
    public IHttpActionResult GetSessionStatus(Guid sessionId)
    {
        return _orchestrator.TryGetStatus(sessionId, out LiveSessionStatusDto status) switch
        {
            LiveLookupResult.Ok => Ok(status),
            _ => Content(HttpStatusCode.NotFound, $"Session \"{sessionId}\" was not found.")
        };
    }

    /// <summary>
    /// Starts a live capture session. The session runs in the background - poll <c>GET
    /// sessions/{sessionId}</c> for progress.
    /// </summary>
    /// <param name="request">Target device, protocol and timing parameters for the capture.</param>
    /// <response code="201">The session was created and is now running in the background.</response>
    /// <response code="400">
    /// The request is malformed (missing/invalid fields or an unrecognized protocol).
    /// </response>
    /// <response code="503">The PMU Connection Tester could not even begin attempting a connection.</response>
    [HttpPost]
    [Route("sessions")]
    [ResponseType(typeof(LiveSessionDto))]
    public IHttpActionResult StartSession(StartLiveSessionRequest request)
    {
        Guid correlationId = Guid.NewGuid();

        if (request is null || !ModelState.IsValid)
            return BadRequestMessage(correlationId, ModelStateErrorMessage());

        if (!TryParseTransportProtocol(request.TransportProtocol, out TransportProtocol transportProtocol))
            return BadRequestMessage(correlationId, $"Invalid transport protocol \"{request.TransportProtocol}\". Only \"Tcp\" and \"Udp\" are supported.");

        if (!Enum.TryParse(request.PhasorProtocol, true, out PhasorProtocol phasorProtocol))
            return BadRequestMessage(correlationId, $"Invalid protocol \"{request.PhasorProtocol}\".");

        ApiLogger.Start(correlationId, "live/sessions", JsonConvert.SerializeObject(request));

        LiveCaptureRequest captureRequest = new()
        {
            PhasorProtocol = phasorProtocol,
            TransportProtocol = transportProtocol,
            ConnectionString = BuildConnectionString(transportProtocol, request.Host, request.Port),
            DeviceIdCode = request.DeviceIdCode,
            FrameRate = request.FrameRate ?? ApiSettings.LiveDefaultFrameRate,
            MinCommunicationWaitSeconds = ApiSettings.ResolveMinCommunicationWaitSeconds(request.MinCommunicationWaitSeconds),
            CaptureDurationSeconds = ApiSettings.ResolveCaptureDurationSeconds(request.CaptureDurationSeconds)
        };

        try
        {
            LiveSessionDto session = _orchestrator.Start(captureRequest);
            ApiLogger.Result(correlationId, "STARTED", 0, 0, $"sessionId={session.SessionId}");
            return Content(HttpStatusCode.Created, session);
        }
        catch (Exception ex)
        {
            ApiLogger.Result(correlationId, "UNAVAILABLE", 0, 0, ex.Message);
            return Content(HttpStatusCode.ServiceUnavailable, ex.Message);
        }
    }

    /// <summary>
    /// Cancels a session, stopping the underlying connection and freeing its resources. Idempotent
    /// - calling this on an already-finished session still returns success.
    /// </summary>
    /// <param name="sessionId">Identifier returned by <see cref="StartSession"/>.</param>
    /// <response code="204">The session was cancelled (or had already finished).</response>
    /// <response code="404">The session never existed.</response>
    [HttpDelete]
    [Route("sessions/{sessionId}")]
    public IHttpActionResult StopSession(Guid sessionId)
    {
        return _orchestrator.Stop(sessionId)
            ? StatusCode(HttpStatusCode.NoContent)
            : Content(HttpStatusCode.NotFound, $"Session \"{sessionId}\" was not found.");
    }

    private static string BuildConnectionString(TransportProtocol transportProtocol, string host, int port) =>
        transportProtocol == TransportProtocol.Tcp
            ? $"server={host}; port={port}; interface=0.0.0.0; islistener=false"
            : $"localport={port}; server={host}; remoteport={port}; interface=0.0.0.0";

    private static bool TryParseTransportProtocol(string transportProtocol, out TransportProtocol result) =>
        Enum.TryParse(transportProtocol, true, out result) && result is TransportProtocol.Tcp or TransportProtocol.Udp;

    private IHttpActionResult BadRequestMessage(Guid correlationId, string message)
    {
        ApiLogger.Start(correlationId, "live/sessions", message);
        ApiLogger.Result(correlationId, "BAD_REQUEST", 0, 0, message);

        return Content(HttpStatusCode.BadRequest, message);
    }

    private string ModelStateErrorMessage()
    {
        foreach (var state in ModelState.Values)
        {
            foreach (var error in state.Errors)
            {
                if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
                    return error.ErrorMessage;
            }
        }

        return "Invalid request parameters.";
    }
}