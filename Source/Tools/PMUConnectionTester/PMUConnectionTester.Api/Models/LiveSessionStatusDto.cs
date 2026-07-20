using System;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Response body for <c>GET /api/pmuconnectiontester/live/sessions/{sessionId}</c>.
/// </summary>
public class LiveSessionStatusDto
{
    /// <summary>
    /// Descriptive failure message, set only when <see cref="Status"/> is <see cref="LiveSessionStatus.Erro"/>.
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// UTC timestamp the session reached a terminal state, when it has.
    /// </summary>
    public DateTime? FinishedAt { get; set; }

    /// <summary>
    /// Identifier of the session.
    /// </summary>
    public Guid SessionId { get; set; }

    /// <summary>
    /// UTC timestamp the session started running.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Overall status of the session.
    /// </summary>
    public LiveSessionStatus Status { get; set; }

    /// <summary>
    /// Step the session is currently in, or last reached before erroring out.
    /// </summary>
    public LiveSessionStep Step { get; set; }
}