namespace ConnectionTester.Api.Models;

/// <summary>
/// "Data" section of the standard test response envelope.
/// </summary>
public class DataMetrics
{
    /// <summary>Total number of frames received of any type (data, configuration, header, command).</summary>
    public long TotalFramesReceived { get; set; }

    /// <summary>Number of data frames received - the count checked against <see cref="MinimumFramesRequired"/>.</summary>
    public int DataFramesReceived { get; set; }

    /// <summary>Number of frames detected as missing based on sequence/timestamp gaps.</summary>
    public long TotalMissingFrames { get; set; }

    /// <summary>Number of frames discarded due to a CRC checksum failure.</summary>
    public long TotalCrcExceptions { get; set; }

    /// <summary>Number of exceptions raised while parsing received frames.</summary>
    public int ParsingExceptions { get; set; }

    /// <summary>Actual frame rate calculated from received data frame timestamps.</summary>
    public double CalculatedFrameRate { get; set; }

    /// <summary>Whether the underlying connection was established and active when the test ended.</summary>
    public bool IsConnected { get; set; }

    /// <summary>Minimum data frame count that had to be received for the test to PASS.</summary>
    public int MinimumFramesRequired { get; set; }
}
