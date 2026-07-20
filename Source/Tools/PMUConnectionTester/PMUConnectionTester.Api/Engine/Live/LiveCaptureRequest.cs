using GSF.Communication;
using GSF.PhasorProtocols;

namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Transport-agnostic input to <see cref="LiveCaptureEngine"/>, built by the orchestrator from the
/// public <c>StartLiveSessionRequest</c> DTO. Mirrors <see cref="PmuTestRequest"/>'s separation of
/// connection-string-building from engine orchestration.
/// </summary>
internal class LiveCaptureRequest
{
    /// <summary>
    /// Only meaningful for <see cref="GSF.Communication.TransportProtocol.File"/> playback (used by
    /// tests to drive the engine against a sample capture) - the live controller never sets this
    /// since a real device connection isn't file playback.
    /// </summary>
    public bool AutoRepeat { get; set; }

    public int CaptureDurationSeconds { get; set; }
    public string ConnectionString { get; set; }
    public ushort DeviceIdCode { get; set; }
    public int FrameRate { get; set; }
    public int MinCommunicationWaitSeconds { get; set; }
    public PhasorProtocol PhasorProtocol { get; set; }

    public TransportProtocol TransportProtocol { get; set; }
}