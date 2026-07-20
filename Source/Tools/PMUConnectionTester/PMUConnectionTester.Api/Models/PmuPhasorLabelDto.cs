namespace ConnectionTester.Api.Models;

/// <summary>
/// A single phasor channel label from a device's configuration frame.
/// </summary>
public class PmuPhasorLabelDto
{
    /// <summary>
    /// Phasor channel label as reported by the device (e.g. "VA GOIGU_230").
    /// </summary>
    public string Label { get; set; }
}