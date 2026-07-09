using GSF.Communication;
using GSF.PhasorProtocols;

namespace ConnectionTester.Api.Engine;

/// <summary>
/// Transport-agnostic input to <see cref="PmuConnectionTestEngine"/>, built by the controller from the
/// endpoint-specific request DTO.
/// </summary>
internal class PmuTestRequest
{
    public PhasorProtocol PhasorProtocol { get; set; }

    public TransportProtocol TransportProtocol { get; set; }

    public string ConnectionString { get; set; }

    public ushort DeviceIdCode { get; set; }

    public int FrameRate { get; set; }

    public bool AutoRepeat { get; set; }

    public int MinimumFrames { get; set; }

    public int TimeoutSeconds { get; set; }
}
