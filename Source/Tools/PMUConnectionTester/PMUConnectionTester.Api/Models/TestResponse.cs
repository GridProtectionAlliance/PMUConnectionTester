namespace ConnectionTester.Api.Models;

/// <summary>
/// Standard response envelope returned by every PMU Connection Tester API endpoint.
/// </summary>
public class TestResponse
{
    /// <summary>Details parsed from the device's configuration frame, if one was received.</summary>
    public ConfigurationInfo Configuration { get; set; }

    /// <summary>Frame counts and connection metrics collected during the test.</summary>
    public DataMetrics Data { get; set; }

    /// <summary>Total wall-clock time the test took to run, in milliseconds.</summary>
    public long ExecutionTimeMs { get; set; }

    /// <summary>"PASS" or "FAIL".</summary>
    public string OverallStatus { get; set; }

    /// <summary>Human-readable explanation of the outcome (success detail or failure reason).</summary>
    public string Message { get; set; }
}
