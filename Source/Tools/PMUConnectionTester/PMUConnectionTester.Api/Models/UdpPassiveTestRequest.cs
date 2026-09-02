using System.ComponentModel.DataAnnotations;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Request body for <c>POST /api/pmuconnectiontester/udp/passive</c>. Used when "Enable Multicast /
/// Remote Udp" is disabled - the API only binds and listens on <see cref="LocalPort"/>, accepting
/// data from any source, without dialing or filtering by a remote host/port.
/// </summary>
public class UdpPassiveTestRequest
{
    /// <summary>Local UDP port the API binds to receive data.</summary>
    [Range(1, 65535)]
    public int LocalPort { get; set; }

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
