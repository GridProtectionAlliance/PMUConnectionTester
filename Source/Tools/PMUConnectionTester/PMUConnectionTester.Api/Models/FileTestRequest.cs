using System.ComponentModel.DataAnnotations;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Request body for <c>POST /api/pmuconnectiontester/file</c>.
/// </summary>
public class FileTestRequest
{
    /// <summary>Path to a ".PmuCapture" file, relative to the API's working directory or absolute.</summary>
    [Required]
    public string Filename { get; set; }

    /// <summary>Expected PMU/PDC device ID code, used to label the response; not validated against the capture.</summary>
    public ushort DeviceIdCode { get; set; }

    /// <summary>Nominal frame rate (frames/second) of the captured data, used to label the response.</summary>
    public int FrameRate { get; set; } = 30;

    /// <summary>One of the <see cref="GSF.PhasorProtocols.PhasorProtocol"/> enumeration names (e.g. "IEEE1344", "IEEEC37_118V1").</summary>
    [Required]
    public string Protocol { get; set; }

    /// <summary>When true, the capture file is played back in a continuous loop.</summary>
    public bool AutoRepeat { get; set; } = true;

    /// <summary>Overrides the configured default minimum frame count required for a PASS result.</summary>
    public int? MinimumFrames { get; set; }

    /// <summary>Overrides the configured default execution timeout, capped by the global maximum.</summary>
    public int? TimeoutSeconds { get; set; }
}
