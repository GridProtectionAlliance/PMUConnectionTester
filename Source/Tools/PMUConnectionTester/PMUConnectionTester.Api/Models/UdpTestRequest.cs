using System.ComponentModel.DataAnnotations;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Request body for <c>POST /api/pmuconnectiontester/udp</c>.
/// </summary>
public class UdpTestRequest
{
    /// <summary>Target PMU/PDC hostname or IP address that data is expected from.</summary>
    [Required]
    public string Host { get; set; }

    /// <summary>Local UDP port the API binds to receive data.</summary>
    [Range(1, 65535)]
    public int LocalPort { get; set; }

    /// <summary>Remote UDP port on <see cref="Host"/> that data is expected from.</summary>
    [Range(1, 65535)]
    public int RemotePort { get; set; }

    /// <summary>Expected PMU/PDC device ID code, used to label the response.</summary>
    public ushort DeviceIdCode { get; set; }

    /// <summary>Nominal frame rate (frames/second) expected from the device, used to label the response.</summary>
    public int FrameRate { get; set; } = 30;

    /// <summary>One of the <see cref="GSF.PhasorProtocols.PhasorProtocol"/> enumeration names (e.g. "IEEE1344", "IEEEC37_118V1").</summary>
    [Required]
    public string Protocol { get; set; }

    /// <summary>Overrides the configured default minimum frame count required for a PASS result.</summary>
    public int? MinimumFrames { get; set; }

    /// <summary>Overrides the configured default execution timeout, capped by the global maximum.</summary>
    public int? TimeoutSeconds { get; set; }
}
