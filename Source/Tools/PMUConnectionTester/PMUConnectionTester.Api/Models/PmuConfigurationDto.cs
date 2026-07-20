using System.Collections.Generic;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Response body for <c>GET /api/pmuconnectiontester/live/sessions/{sessionId}/configuration</c> -
/// the configuration (CFG) frame received from the device.
/// </summary>
public class PmuConfigurationDto
{
    /// <summary>
    /// One entry per device (PMU) described by the configuration frame.
    /// </summary>
    public List<PmuConfigurationCellDto> Cells { get; set; } = new();

    /// <summary>
    /// Frame rate reported by the device's configuration frame.
    /// </summary>
    public int FrameRate { get; set; }

    /// <summary>
    /// "Success" once a configuration frame has been received and parsed.
    /// </summary>
    public string Result { get; set; }
}