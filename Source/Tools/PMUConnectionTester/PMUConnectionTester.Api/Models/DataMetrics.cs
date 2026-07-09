namespace ConnectionTester.Api.Models;

/// <summary>
/// "Data" section of the standard test response envelope.
/// </summary>
public class DataMetrics
{
    public long TotalFramesReceived { get; set; }

    public int DataFramesReceived { get; set; }

    public long TotalMissingFrames { get; set; }

    public long TotalCrcExceptions { get; set; }

    public int ParsingExceptions { get; set; }

    public double CalculatedFrameRate { get; set; }

    public bool IsConnected { get; set; }

    public int MinimumFramesRequired { get; set; }
}
