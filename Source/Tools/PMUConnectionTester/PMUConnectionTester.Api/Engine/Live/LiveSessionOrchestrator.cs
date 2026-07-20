using ConnectionTester.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Default <see cref="ILiveSessionOrchestrator"/> - registers a new <see cref="LiveSession"/> in
/// the process-wide <see cref="LiveSessionStore"/> and starts its capture on a dedicated background thread.
/// </summary>
internal class LiveSessionOrchestrator : ILiveSessionOrchestrator
{
    private readonly ILiveCaptureEngine _engine;
    private readonly LiveSessionStore _store;

    public LiveSessionOrchestrator() : this(LiveSessionStore.Instance, new LiveCaptureEngine())
    {
    }

    internal LiveSessionOrchestrator(LiveSessionStore store, ILiveCaptureEngine engine)
    {
        _store = store;
        _engine = engine;
    }

    public LiveSessionDto Start(LiveCaptureRequest request)
    {
        LiveSession session = new();
        _store.Add(session);

        Task.Run(() => _engine.Run(session, request));

        return new LiveSessionDto
        {
            SessionId = session.SessionId,
            Step = session.Step,
            Status = session.Status,
            CreatedAt = session.CreatedAt
        };
    }

    public bool Stop(Guid sessionId)
    {
        if (!_store.TryGet(sessionId, out LiveSession session))
            return false;

        session.Cancel();
        return true;
    }

    public LiveLookupResult TryGetConfiguration(Guid sessionId, out PmuConfigurationDto configuration)
    {
        configuration = null;

        if (!_store.TryGet(sessionId, out LiveSession session))
            return LiveLookupResult.NotFound;

        return session.TryGetConfiguration(out configuration) ? LiveLookupResult.Ok : LiveLookupResult.NotReady;
    }

    public LiveLookupResult TryGetMeasurements(Guid sessionId, out List<PmuMeasurementSeriesDto> measurements)
    {
        measurements = null;

        if (!_store.TryGet(sessionId, out LiveSession session))
            return LiveLookupResult.NotFound;

        return session.TryGetMeasurements(out measurements) ? LiveLookupResult.Ok : LiveLookupResult.NotReady;
    }

    public LiveLookupResult TryGetStatus(Guid sessionId, out LiveSessionStatusDto status)
    {
        status = null;

        if (!_store.TryGet(sessionId, out LiveSession session))
            return LiveLookupResult.NotFound;

        status = session.ToStatusDto();
        return LiveLookupResult.Ok;
    }
}