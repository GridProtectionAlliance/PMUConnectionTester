namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Abstraction over <see cref="LiveCaptureEngine"/> so the orchestrator's engine dependency can be
/// substituted with a test double instead of driving a real connection/capture.
/// </summary>
internal interface ILiveCaptureEngine
{
    /// <summary>
    /// Runs the connect/wait/capture/close state machine for <paramref name="session"/>, mutating
    /// it as the session progresses. Returns once the session reaches a terminal state (
    /// <c>Concluido</c> or <c>Erro</c>) or cancellation is requested.
    /// </summary>
    void Run(LiveSession session, LiveCaptureRequest request);
}