namespace ConnectionTester.Api.Models;

/// <summary>
/// "Configuration" section of the standard test response envelope.
/// </summary>
public class ConfigurationInfo
{
    public bool ConfigurationFrameReceived { get; set; }

    public ushort DeviceIdCode { get; set; }

    public int FrameRate { get; set; }

    public int? ReportedFrameRate { get; set; }

    public string Protocol { get; set; }

    public int? DeviceCount { get; set; }
}
