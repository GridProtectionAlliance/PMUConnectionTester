using System;

namespace ConnectionTester.Api.Models;

/// <summary>
/// Response body for <c>POST /api/pmuconnectiontester/live/sessions</c>.
/// </summary>
public class LiveSessionDto
{
    /// <summary>
    /// UTC timestamp the session was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Identifier used to poll/query/cancel this session.
    /// </summary>
    public Guid SessionId { get; set; }

    /// <summary>
    /// Overall status of the session.
    /// </summary>
    public LiveSessionStatus Status { get; set; }

    /// <summary>
    /// Step the session is currently in.
    /// </summary>
    public LiveSessionStep Step { get; set; }
}