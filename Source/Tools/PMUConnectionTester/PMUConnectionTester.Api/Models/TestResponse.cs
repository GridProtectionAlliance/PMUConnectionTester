namespace ConnectionTester.Api.Models;

/// <summary>
/// Standard response envelope returned by every PMU Connection Tester API endpoint.
/// </summary>
public class TestResponse
{
    public ConfigurationInfo Configuration { get; set; }

    public DataMetrics Data { get; set; }

    public long ExecutionTimeMs { get; set; }

    /// <summary>"PASS" or "FAIL".</summary>
    public string OverallStatus { get; set; }

    public string Message { get; set; }
}
