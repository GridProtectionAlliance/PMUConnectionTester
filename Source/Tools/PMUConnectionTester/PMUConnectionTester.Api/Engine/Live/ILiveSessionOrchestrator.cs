using ConnectionTester.Api.Models;
using System;
using System.Collections.Generic;

namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Outcome of a session lookup - lets the controller distinguish "never existed"/"expired" (404)
/// from "exists but the requested data isn't ready yet" (409) without exceptions for control flow,
/// matching the existing <c>CompletionReason</c>-style convention.
/// </summary>
internal enum LiveLookupResult
{
    Ok,
    NotFound,
    NotReady
}

/// <summary>
/// Thin layer the controller talks to - wraps <see cref="LiveSessionStore"/> and <see
/// cref="ILiveCaptureEngine"/> so the controller stays unit-testable via Moq, mirroring <c>IPmuConnectionTestEngine</c>.
/// </summary>
internal interface ILiveSessionOrchestrator
{
    LiveSessionDto Start(LiveCaptureRequest request);

    /// <summary>
    /// Cancels/removes a session. Returns false only if the session never existed.
    /// </summary>
    bool Stop(Guid sessionId);

    LiveLookupResult TryGetConfiguration(Guid sessionId, out PmuConfigurationDto configuration);

    LiveLookupResult TryGetMeasurements(Guid sessionId, out List<PmuMeasurementSeriesDto> measurements);

    LiveLookupResult TryGetStatus(Guid sessionId, out LiveSessionStatusDto status);
}