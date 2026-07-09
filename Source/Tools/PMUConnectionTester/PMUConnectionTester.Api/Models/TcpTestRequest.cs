using System.ComponentModel.DataAnnotations;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Request body for <c>POST /api/pmuconnectiontester/tcp</c>.
/// </summary>
public class TcpTestRequest
{
    [Required]
    public string Host { get; set; }

    [Range(1, 65535)]
    public int Port { get; set; }

    public ushort DeviceIdCode { get; set; }

    public int FrameRate { get; set; } = 30;

    [Required]
    public string Protocol { get; set; }

    /// <summary>When true, the API listens for an inbound TCP connection instead of dialing out to <see cref="Host"/>/<see cref="Port"/>.</summary>
    public bool IsListener { get; set; }

    public int? MinimumFrames { get; set; }

    public int? TimeoutSeconds { get; set; }
}
