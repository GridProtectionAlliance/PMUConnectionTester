using System;
using System.Collections.Generic;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Response entry for <c>GET /api/pmuconnectiontester/live/sessions/{sessionId}/measurements</c> -
/// the captured time series for a single device.
/// </summary>
public class PmuMeasurementSeriesDto
{
    /// <summary>
    /// UTC timestamp the capture window ended.
    /// </summary>
    public DateTime CaptureEndTime { get; set; }

    /// <summary>
    /// UTC timestamp the capture window started.
    /// </summary>
    public DateTime CaptureStartTime { get; set; }

    /// <summary>
    /// Length of the capture window, in seconds.
    /// </summary>
    public int FpsWindowSeconds { get; set; }

    /// <summary>
    /// idCode of the device, as a string.
    /// </summary>
    public string IdPmu { get; set; }

    /// <summary>
    /// One entry per data frame received for this device.
    /// </summary>
    public List<PmuMeasurementSampleDto> Measurements { get; set; } = new();

    /// <summary>
    /// Total number of data frames received for this device during the capture window.
    /// </summary>
    public long ReceivedFrameCount { get; set; }
}