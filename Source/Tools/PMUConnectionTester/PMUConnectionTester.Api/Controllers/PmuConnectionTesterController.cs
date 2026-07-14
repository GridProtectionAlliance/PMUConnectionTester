using ConnectionTester.Api.Engine;
using ConnectionTester.Api.Infrastructure;
using ConnectionTester.Api.Models;
using GSF.Communication;
using GSF.PhasorProtocols;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace ConnectionTester.Api.Controllers;

/// <summary>
/// Synchronous PMU connectivity test endpoints - file playback, TCP and UDP.
/// </summary>
[RoutePrefix("api/pmuconnectiontester")]
public class PmuConnectionTesterController : ApiController
{
    private readonly IPmuConnectionTestEngine _engine;

    public PmuConnectionTesterController() : this(new PmuConnectionTestEngine())
    {
    }

    internal PmuConnectionTesterController(IPmuConnectionTestEngine engine)
    {
        _engine = engine;
    }

    /// <summary>
    /// Runs a connectivity test by replaying a captured ".PmuCapture" file.
    /// </summary>
    /// <param name="request">File path, protocol and pass/fail thresholds for the playback test.</param>
    /// <response code="200">
    /// The test ran to completion; check <see cref="TestResponse.OverallStatus"/> for PASS/FAIL.
    /// </response>
    /// <response code="400">
    /// The request is malformed (missing/invalid fields or an unrecognized protocol).
    /// </response>
    [HttpPost]
    [Route("file")]
    [ResponseType(typeof(TestResponse))]
    public IHttpActionResult File(FileTestRequest request)
    {
        Guid correlationId = Guid.NewGuid();

        if (request is null || !ModelState.IsValid)
            return BadRequestResponse(correlationId, "file", ModelStateErrorMessage());

        if (!TryParseProtocol(request.Protocol, out PhasorProtocol protocol))
            return BadRequestResponse(correlationId, "file", $"Invalid protocol \"{request.Protocol}\".");

        ApiLogger.Start(correlationId, "file", JsonConvert.SerializeObject(request));

        if (!System.IO.File.Exists(request.Filename))
        {
            TestResponse failResponse = BuildFailResponse($"File not found: {request.Filename}", protocol, request.DeviceIdCode, request.FrameRate);
            ApiLogger.Result(correlationId, failResponse.OverallStatus, failResponse.ExecutionTimeMs, 0, failResponse.Message);
            return Ok(failResponse);
        }

        PmuTestRequest engineRequest = new()
        {
            PhasorProtocol = protocol,
            TransportProtocol = TransportProtocol.File,
            ConnectionString = $"file={request.Filename}",
            DeviceIdCode = request.DeviceIdCode,
            FrameRate = request.FrameRate,
            AutoRepeat = request.AutoRepeat,
            MinimumFrames = ApiSettings.ResolveMinimumFrames(request.MinimumFrames),
            TimeoutSeconds = ApiSettings.ResolveTimeoutSeconds(request.TimeoutSeconds)
        };

        return RunAndRespond(correlationId, engineRequest, protocol, request.DeviceIdCode, request.FrameRate);
    }

    /// <summary>
    /// Runs a connectivity test against a TCP host/port.
    /// </summary>
    /// <param name="request">Target host/port, protocol and pass/fail thresholds for the TCP test.</param>
    /// <response code="200">
    /// The test ran to completion; check <see cref="TestResponse.OverallStatus"/> for PASS/FAIL.
    /// </response>
    /// <response code="400">
    /// The request is malformed (missing/invalid fields or an unrecognized protocol).
    /// </response>
    [HttpPost]
    [Route("tcp")]
    [ResponseType(typeof(TestResponse))]
    public IHttpActionResult Tcp(TcpTestRequest request)
    {
        Guid correlationId = Guid.NewGuid();

        if (request is null || !ModelState.IsValid)
            return BadRequestResponse(correlationId, "tcp", ModelStateErrorMessage());

        if (!TryParseProtocol(request.Protocol, out PhasorProtocol protocol))
            return BadRequestResponse(correlationId, "tcp", $"Invalid protocol \"{request.Protocol}\".");

        ApiLogger.Start(correlationId, "tcp", JsonConvert.SerializeObject(request));

        PmuTestRequest engineRequest = new()
        {
            PhasorProtocol = protocol,
            TransportProtocol = TransportProtocol.Tcp,
            ConnectionString = $"server={request.Host}; port={request.Port}; interface=0.0.0.0; islistener={request.IsListener}",
            DeviceIdCode = request.DeviceIdCode,
            FrameRate = request.FrameRate,
            AutoRepeat = false,
            MinimumFrames = ApiSettings.ResolveMinimumFrames(request.MinimumFrames),
            TimeoutSeconds = ApiSettings.ResolveTimeoutSeconds(request.TimeoutSeconds)
        };

        return RunAndRespond(correlationId, engineRequest, protocol, request.DeviceIdCode, request.FrameRate);
    }

    /// <summary>
    /// Runs a connectivity test against a UDP host/port pair.
    /// </summary>
    /// <param name="request">
    /// Local/remote ports, target host, protocol and pass/fail thresholds for the UDP test.
    /// </param>
    /// <response code="200">
    /// The test ran to completion; check <see cref="TestResponse.OverallStatus"/> for PASS/FAIL.
    /// </response>
    /// <response code="400">
    /// The request is malformed (missing/invalid fields or an unrecognized protocol).
    /// </response>
    [HttpPost]
    [Route("udp")]
    [ResponseType(typeof(TestResponse))]
    public IHttpActionResult Udp(UdpTestRequest request)
    {
        Guid correlationId = Guid.NewGuid();

        if (request is null || !ModelState.IsValid)
            return BadRequestResponse(correlationId, "udp", ModelStateErrorMessage());

        if (!TryParseProtocol(request.Protocol, out PhasorProtocol protocol))
            return BadRequestResponse(correlationId, "udp", $"Invalid protocol \"{request.Protocol}\".");

        ApiLogger.Start(correlationId, "udp", JsonConvert.SerializeObject(request));

        PmuTestRequest engineRequest = new()
        {
            PhasorProtocol = protocol,
            TransportProtocol = TransportProtocol.Udp,
            ConnectionString = $"localport={request.LocalPort}; server={request.Host}; remoteport={request.RemotePort}; interface=0.0.0.0",
            DeviceIdCode = request.DeviceIdCode,
            FrameRate = request.FrameRate,
            AutoRepeat = false,
            MinimumFrames = ApiSettings.ResolveMinimumFrames(request.MinimumFrames),
            TimeoutSeconds = ApiSettings.ResolveTimeoutSeconds(request.TimeoutSeconds)
        };

        return RunAndRespond(correlationId, engineRequest, protocol, request.DeviceIdCode, request.FrameRate);
    }

    private static TestResponse BuildFailResponse(string message, PhasorProtocol protocol, ushort deviceIdCode, int frameRate) =>
        new()
        {
            Configuration = new ConfigurationInfo
            {
                ConfigurationFrameReceived = false,
                DeviceIdCode = deviceIdCode,
                FrameRate = frameRate,
                Protocol = protocol.ToString()
            },
            Data = new DataMetrics(),
            ExecutionTimeMs = 0,
            OverallStatus = "FAIL",
            Message = message
        };

    private static TestResponse MapOutcome(PmuTestOutcome outcome, PhasorProtocol protocol, ushort deviceIdCode, int frameRate)
    {
        bool pass = outcome.Completion == CompletionReason.MinimumFramesReached && outcome.ParsingExceptions == 0;

        string message = outcome.Completion switch
        {
            CompletionReason.MinimumFramesReached when pass =>
                $"Connectivity test completed successfully: {outcome.DataFramesReceived} frames received, 0 parsing exceptions.",
            CompletionReason.MinimumFramesReached =>
                $"Minimum frame count reached but {outcome.ParsingExceptions} parsing exception(s) were encountered.",
            CompletionReason.ParsingExceptionThresholdExceeded =>
                "Exceeded the allowed parsing exception threshold.",
            CompletionReason.ConnectionError =>
                $"Connection failed: {outcome.LastError?.Message ?? "unknown error"}",
            CompletionReason.PlaybackEnded =>
                $"Capture playback ended before the minimum frame count ({outcome.MinimumFramesRequired}) was reached; received {outcome.DataFramesReceived} frame(s).",
            CompletionReason.Timeout =>
                $"Execution timeout exceeded; received {outcome.DataFramesReceived} of {outcome.MinimumFramesRequired} required frames.",
            _ => "Unknown outcome."
        };

        return new TestResponse
        {
            Configuration = new ConfigurationInfo
            {
                ConfigurationFrameReceived = outcome.ConfigurationFrameReceived,
                DeviceIdCode = deviceIdCode,
                FrameRate = frameRate,
                ReportedFrameRate = outcome.ConfigurationFrame?.FrameRate,
                Protocol = protocol.ToString(),
                DeviceCount = outcome.ConfigurationFrame?.Cells?.Count
            },
            Data = new DataMetrics
            {
                TotalFramesReceived = outcome.TotalFramesReceived,
                DataFramesReceived = outcome.DataFramesReceived,
                TotalMissingFrames = outcome.TotalMissingFrames,
                TotalCrcExceptions = outcome.TotalCrcExceptions,
                ParsingExceptions = outcome.ParsingExceptions,
                CalculatedFrameRate = outcome.CalculatedFrameRate,
                IsConnected = outcome.IsConnected,
                MinimumFramesRequired = outcome.MinimumFramesRequired
            },
            ExecutionTimeMs = outcome.ElapsedMilliseconds,
            OverallStatus = pass ? "PASS" : "FAIL",
            Message = message
        };
    }

    private static bool TryParseProtocol(string protocol, out PhasorProtocol result) =>
        Enum.TryParse(protocol, true, out result);

    private IHttpActionResult BadRequestResponse(Guid correlationId, string endpoint, string message)
    {
        TestResponse response = new()
        {
            Configuration = new ConfigurationInfo(),
            Data = new DataMetrics(),
            ExecutionTimeMs = 0,
            OverallStatus = "FAIL",
            Message = message
        };

        ApiLogger.Start(correlationId, endpoint, message);
        ApiLogger.Result(correlationId, response.OverallStatus, 0, 0, response.Message);

        return Content(HttpStatusCode.BadRequest, response);
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

    private IHttpActionResult RunAndRespond(Guid correlationId, PmuTestRequest engineRequest, PhasorProtocol protocol, ushort deviceIdCode, int frameRate)
    {
        PmuTestOutcome outcome = _engine.Run(engineRequest);
        TestResponse response = MapOutcome(outcome, protocol, deviceIdCode, frameRate);

        ApiLogger.Result(correlationId, response.OverallStatus, response.ExecutionTimeMs, outcome.DataFramesReceived, response.Message);

        return Ok(response);
    }
}