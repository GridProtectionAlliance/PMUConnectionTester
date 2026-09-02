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
    /// UTC instant this frame was actually received and parsed by this process (stamped in
    /// <c>LiveCaptureEngine.RecordDataFrame</c>, as the frame arrives from <see
    /// cref="GSF.PhasorProtocols.MultiProtocolFrameParser"/>) - distinct from <see
    /// cref="Timestamp"/>, which is the source timestamp embedded by the PMU itself. The difference
    /// between the two is the real per-frame latency (device to this process), which the consumer
    /// (GestorBase) uses instead of comparing every sample in a capture window against a single
    /// "now" taken after the whole session completes.
    /// </summary>
    public DateTime ReceivedAtUtc { get; set; }

    /// <summary>
    /// Rate of change of frequency (ROCOF), in Hz/s.
    /// </summary>
    public double Rocof { get; set; }

    /// <summary>
    /// Status flags word (STAT) reported in this data frame for the device. Unlike <see
    /// cref="PmuConfigurationCellDto.Stat"/> - which is always 0, since the field doesn't exist on
    /// the configuration frame - this is the real value reported per data frame.
    /// </summary>
    public int Stat { get; set; }

    /// <summary>
    /// UTC timestamp of the data frame, as embedded by the PMU.
    /// </summary>
    public DateTime Timestamp { get; set; }
}