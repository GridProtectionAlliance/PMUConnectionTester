using System.ComponentModel.DataAnnotations;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Request body for <c>POST /api/pmuconnectiontester/udp</c>.
/// </summary>
public class UdpTestRequest
{
    [Required]
    public string Host { get; set; }

    [Range(1, 65535)]
    public int LocalPort { get; set; }

    [Range(1, 65535)]
    public int RemotePort { get; set; }

    public ushort DeviceIdCode { get; set; }

    public int FrameRate { get; set; } = 30;

    [Required]
    public string Protocol { get; set; }

    public int? MinimumFrames { get; set; }

    public int? TimeoutSeconds { get; set; }
}
