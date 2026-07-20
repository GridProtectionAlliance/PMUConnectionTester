using System;
using System.Collections.Generic;

namespace ConnectionTester.Api.Models;

/// <summary>
/// A single data-frame's worth of measurements for one device.
/// </summary>
public class PmuMeasurementSampleDto
{
    /// <summary>
    /// Active power, in MW - computed from the first voltage/current phasor pair, when both are present.
    /// </summary>
    public double? ActivePower { get; set; }

    /// <summary>
    /// Frequency, in Hz.
    /// </summary>
    public double Frequency { get; set; }

    /// <summary>
    /// Phasor values reported in this frame.
    /// </summary>
    public List<PmuPhasorMeasurementDto> Phasors { get; set; } = new();

    /// <summary>
    /// Reactive power, in MVars - computed from the first voltage/current phasor pair, when both
    /// are present.
    /// </summary>
    public double? ReactivePower { get; set; }

    /// <summary>
    /// Rate of change of frequency (ROCOF), in Hz/s.
    /// </summary>
    public double Rocof { get; set; }

    /// <summary>
    /// UTC timestamp of the data frame.
    /// </summary>
    public DateTime Timestamp { get; set; }
}