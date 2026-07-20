using System.ComponentModel.DataAnnotations;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Request body for <c>POST /api/pmuconnectiontester/live/sessions</c>.
/// </summary>
public class StartLiveSessionRequest
{
    /// <summary>
    /// Duration of the capture window. Clamped to at most the configured ceiling.
    /// </summary>
    public int? CaptureDurationSeconds { get; set; }

    /// <summary>
    /// idCode of the target device.
    /// </summary>
    public ushort DeviceIdCode { get; set; }

    /// <summary>
    /// Expected frame rate (frames/second). Falls back to <c>LiveDefaultFrameRate</c> when omitted.
    /// </summary>
    public int? FrameRate { get; set; }

    /// <summary>
    /// IP address or hostname of the target PMU/PDC.
    /// </summary>
    [Required]
    public string Host { get; set; }

    /// <summary>
    /// Minimum time to wait for communication before failing. Clamped to at least the configured floor.
    /// </summary>
    public int? MinCommunicationWaitSeconds { get; set; }

    /// <summary>
    /// One of the <see cref="GSF.PhasorProtocols.PhasorProtocol"/> enumeration names (e.g. "IEEEC37_118V2").
    /// </summary>
    [Required]
    public string PhasorProtocol { get; set; }

    /// <summary>
    /// Port to connect to.
    /// </summary>
    [Range(1, 65535)]
    public int Port { get; set; }

    /// <summary>
    /// One of "Tcp" or "Udp" ( <see cref="GSF.Communication.TransportProtocol"/> - other values are rejected).
    /// </summary>
    [Required]
    public string TransportProtocol { get; set; }
}