using System.Collections.Generic;

namespace ConnectionTester.Api.Models;

/// <summary>
/// A single device (PMU) cell from a configuration frame.
/// </summary>
public class PmuConfigurationCellDto
{
    /// <summary>
    /// idCode of the device.
    /// </summary>
    public int IdCode { get; set; }

    /// <summary>
    /// Phasor channels defined for this device.
    /// </summary>
    public List<PmuPhasorLabelDto> Phasors { get; set; } = new();

    /// <summary>
    /// Status flags word reported for the device.
    /// </summary>
    public int Stat { get; set; }

    /// <summary>
    /// Station name reported by the device.
    /// </summary>
    public string StationName { get; set; }
}