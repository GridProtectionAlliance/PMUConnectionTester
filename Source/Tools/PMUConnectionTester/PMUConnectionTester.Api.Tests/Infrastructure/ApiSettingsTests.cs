using ConnectionTester.Api.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMUConnectionTester.Api.Tests.Infrastructure;

/// <summary>
/// Exercises <see cref="ApiSettings"/> defaults (this test assembly's own App.config defines none
/// of these keys, so every read falls back to the documented default) and the Resolve* clamping logic.
/// </summary>
[TestClass]
public class ApiSettingsTests
{
    [TestMethod]
    public void DefaultMinimumFrames_NotConfigured_DefaultsTo30() =>
        Assert.AreEqual(30, ApiSettings.DefaultMinimumFrames);

    [TestMethod]
    public void ListenAddress_NotConfigured_DefaultsToLocalhost() =>
        Assert.AreEqual("localhost", ApiSettings.ListenAddress);

    [TestMethod]
    public void ListenPort_NotConfigured_DefaultsTo8888() =>
        Assert.AreEqual(8888, ApiSettings.ListenPort);

    [TestMethod]
    public void LiveDefaultFrameRate_NotConfigured_DefaultsTo30() =>
        Assert.AreEqual(30, ApiSettings.LiveDefaultFrameRate);

    [TestMethod]
    public void LiveMaxCaptureDurationSeconds_NotConfigured_DefaultsTo120() =>
        Assert.AreEqual(120, ApiSettings.LiveMaxCaptureDurationSeconds);

    [TestMethod]
    public void LiveMinCommunicationWaitSeconds_NotConfigured_DefaultsTo60() =>
        Assert.AreEqual(60, ApiSettings.LiveMinCommunicationWaitSeconds);

    [TestMethod]
    public void MaxExecutionTimeoutSeconds_NotConfigured_DefaultsTo60() =>
        Assert.AreEqual(60, ApiSettings.MaxExecutionTimeoutSeconds);

    [TestMethod]
    public void ResolveCaptureDurationSeconds_NoOverride_ReturnsConfiguredCeiling() =>
        Assert.AreEqual(ApiSettings.LiveMaxCaptureDurationSeconds, ApiSettings.ResolveCaptureDurationSeconds(null));

    [TestMethod]
    public void ResolveCaptureDurationSeconds_OverrideAboveCeiling_IsClampedToCeiling() =>
        Assert.AreEqual(ApiSettings.LiveMaxCaptureDurationSeconds, ApiSettings.ResolveCaptureDurationSeconds(ApiSettings.LiveMaxCaptureDurationSeconds + 30));

    [TestMethod]
    public void ResolveCaptureDurationSeconds_OverrideBelowCeiling_ReturnsOverride() =>
        Assert.AreEqual(5, ApiSettings.ResolveCaptureDurationSeconds(5));

    [TestMethod]
    public void ResolveMinCommunicationWaitSeconds_NoOverride_ReturnsConfiguredFloor() =>
        Assert.AreEqual(ApiSettings.LiveMinCommunicationWaitSeconds, ApiSettings.ResolveMinCommunicationWaitSeconds(null));

    [TestMethod]
    public void ResolveMinCommunicationWaitSeconds_OverrideAboveFloor_ReturnsOverride() =>
        Assert.AreEqual(ApiSettings.LiveMinCommunicationWaitSeconds + 30, ApiSettings.ResolveMinCommunicationWaitSeconds(ApiSettings.LiveMinCommunicationWaitSeconds + 30));

    [TestMethod]
    public void ResolveMinCommunicationWaitSeconds_OverrideBelowFloor_IsClampedToFloor() =>
        Assert.AreEqual(ApiSettings.LiveMinCommunicationWaitSeconds, ApiSettings.ResolveMinCommunicationWaitSeconds(1));

    [TestMethod]
    public void ResolveMinimumFrames_NoOverride_ReturnsConfiguredDefault() =>
        Assert.AreEqual(ApiSettings.DefaultMinimumFrames, ApiSettings.ResolveMinimumFrames(null));

    [TestMethod]
    public void ResolveMinimumFrames_OverrideProvided_ReturnsOverride() =>
        Assert.AreEqual(5, ApiSettings.ResolveMinimumFrames(5));

    [TestMethod]
    public void ResolveTimeoutSeconds_NoOverride_ReturnsConfiguredMaximum() =>
        Assert.AreEqual(ApiSettings.MaxExecutionTimeoutSeconds, ApiSettings.ResolveTimeoutSeconds(null));

    [TestMethod]
    public void ResolveTimeoutSeconds_OverrideAboveMaximum_IsClampedToMaximum() =>
        Assert.AreEqual(ApiSettings.MaxExecutionTimeoutSeconds, ApiSettings.ResolveTimeoutSeconds(ApiSettings.MaxExecutionTimeoutSeconds + 1000));

    [TestMethod]
    public void ResolveTimeoutSeconds_OverrideBelowMaximum_ReturnsOverride() =>
        Assert.AreEqual(10, ApiSettings.ResolveTimeoutSeconds(10));
}