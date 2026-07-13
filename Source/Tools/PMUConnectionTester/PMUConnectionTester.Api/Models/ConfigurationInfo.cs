namespace ConnectionTester.Api.Models;

/// <summary>
/// "Configuration" section of the standard test response envelope.
/// </summary>
public class ConfigurationInfo
{
    /// <summary>Whether a configuration frame was received and successfully parsed during the test.</summary>
    public bool ConfigurationFrameReceived { get; set; }

    /// <summary>Device ID code echoed back from the request.</summary>
    public ushort DeviceIdCode { get; set; }

    /// <summary>Frame rate echoed back from the request.</summary>
    public int FrameRate { get; set; }

    /// <summary>Frame rate actually reported by the device's configuration frame, when one was received.</summary>
    public int? ReportedFrameRate { get; set; }

    /// <summary>Phasor protocol used for the test (e.g. "IEEE1344", "IEEEC37_118V1").</summary>
    public string Protocol { get; set; }

    /// <summary>Number of devices (PMUs) described by the received configuration frame, when one was received.</summary>
    public int? DeviceCount { get; set; }
}
