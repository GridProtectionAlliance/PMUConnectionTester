using System.ComponentModel.DataAnnotations;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Request body for <c>POST /api/pmuconnectiontester/tcp</c>.
/// </summary>
public class TcpTestRequest
{
    /// <summary>Target PMU/PDC hostname or IP address (ignored when <see cref="IsListener"/> is true).</summary>
    [Required]
    public string Host { get; set; }

    /// <summary>TCP port to connect to, or to listen on when <see cref="IsListener"/> is true.</summary>
    [Range(1, 65535)]
    public int Port { get; set; }

    /// <summary>Expected PMU/PDC device ID code, used to label the response.</summary>
    public ushort DeviceIdCode { get; set; }

    /// <summary>Nominal frame rate (frames/second) expected from the device, used to label the response.</summary>
    public int FrameRate { get; set; } = 30;

    /// <summary>One of the <see cref="GSF.PhasorProtocols.PhasorProtocol"/> enumeration names (e.g. "IEEE1344", "IEEEC37_118V1").</summary>
    [Required]
    public string Protocol { get; set; }

    /// <summary>When true, the API listens for an inbound TCP connection instead of dialing out to <see cref="Host"/>/<see cref="Port"/>.</summary>
    public bool IsListener { get; set; }

    /// <summary>Overrides the configured default minimum frame count required for a PASS result.</summary>
    public int? MinimumFrames { get; set; }

    /// <summary>Overrides the configured default execution timeout, capped by the global maximum.</summary>
    public int? TimeoutSeconds { get; set; }
}
