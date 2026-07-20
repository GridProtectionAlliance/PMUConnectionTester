namespace ConnectionTester.Api.Models;

/// <summary>
/// A single phasor value within a measurement sample.
/// </summary>
public class PmuPhasorMeasurementDto
{
    /// <summary>
    /// Adjusted angle, in degrees.
    /// </summary>
    public double Angle { get; set; }

    /// <summary>
    /// Phasor channel label, matching the corresponding <see cref="PmuPhasorLabelDto.Label"/>.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Adjusted magnitude.
    /// </summary>
    public double Magnitude { get; set; }

    /// <summary>
    /// "FV" for a voltage phasor, "FA" for a current phasor.
    /// </summary>
    public string Type { get; set; }
}