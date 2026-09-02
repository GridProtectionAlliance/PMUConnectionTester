namespace ConnectionTester.Api.Engine;

/// <summary>
/// Abstraction over <see cref="PmuConnectionTestEngine"/> so the controller's engine dependency can be
/// substituted with a test double instead of driving a real connection/file playback.
/// </summary>
internal interface IPmuConnectionTestEngine
{
    PmuTestOutcome Run(PmuTestRequest request);
}
