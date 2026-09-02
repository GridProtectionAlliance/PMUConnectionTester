using ConnectionTester.Api.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Process-wide registry of in-flight and recently finished live capture sessions. A periodic sweep
/// removes sessions that finished more than <see cref="ApiSettings.LiveSessionRetentionSeconds"/>
/// ago, so polling an old <c>sessionId</c> eventually 404s ("já expirado/limpo") instead of leaking memory.
/// </summary>
internal class LiveSessionStore
{
    private readonly Timer _cleanupTimer;
    private readonly ConcurrentDictionary<Guid, LiveSession> _sessions = new();

    public LiveSessionStore()
    {
        TimeSpan sweepInterval = TimeSpan.FromSeconds(30);
        _cleanupTimer = new Timer(_ => Sweep(), null, sweepInterval, sweepInterval);
    }

    public static LiveSessionStore Instance { get; } = new();

    public void Add(LiveSession session) =>
        _sessions[session.SessionId] = session;

    public bool Remove(Guid sessionId, out LiveSession session) =>
        _sessions.TryRemove(sessionId, out session);

    public bool TryGet(Guid sessionId, out LiveSession session) =>
            _sessions.TryGetValue(sessionId, out session);

    internal void Sweep()
    {
        DateTime cutoff = DateTime.UtcNow.AddSeconds(-ApiSettings.LiveSessionRetentionSeconds);

        foreach (Guid sessionId in _sessions
            .Where(entry => entry.Value.IsFinished() && entry.Value.ToStatusDto().FinishedAt < cutoff)
            .Select(entry => entry.Key)
            .ToList())
        {
            _sessions.TryRemove(sessionId, out _);
        }
    }
}