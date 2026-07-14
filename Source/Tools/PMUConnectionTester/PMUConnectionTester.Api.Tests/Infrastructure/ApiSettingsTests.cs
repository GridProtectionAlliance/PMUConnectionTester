using ConnectionTester.Api.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMUConnectionTester.Api.Tests.Infrastructure;

/// <summary>
/// Exercises <see cref="ApiSettings"/> defaults (this test assembly's own App.config defines none of
/// these keys, so every read falls back to the documented default) and the Resolve* clamping logic.
/// </summary>
[TestClass]
public class ApiSettingsTests
{
    [TestMethod]
    public void ListenAddress_NotConfigured_DefaultsToLocalhost() =>
        Assert.AreEqual("localhost", ApiSettings.ListenAddress);

    [TestMethod]
    public void ListenPort_NotConfigured_DefaultsTo8888() =>
        Assert.AreEqual(8888, ApiSettings.ListenPort);

    [TestMethod]
    public void MaxExecutionTimeoutSeconds_NotConfigured_DefaultsTo60() =>
        Assert.AreEqual(60, ApiSettings.MaxExecutionTimeoutSeconds);

    [TestMethod]
    public void DefaultMinimumFrames_NotConfigured_DefaultsTo30() =>
        Assert.AreEqual(30, ApiSettings.DefaultMinimumFrames);

    [TestMethod]
    public void ResolveTimeoutSeconds_NoOverride_ReturnsConfiguredMaximum() =>
        Assert.AreEqual(ApiSettings.MaxExecutionTimeoutSeconds, ApiSettings.ResolveTimeoutSeconds(null));

    [TestMethod]
    public void ResolveTimeoutSeconds_OverrideBelowMaximum_ReturnsOverride() =>
        Assert.AreEqual(10, ApiSettings.ResolveTimeoutSeconds(10));

    [TestMethod]
    public void ResolveTimeoutSeconds_OverrideAboveMaximum_IsClampedToMaximum() =>
        Assert.AreEqual(ApiSettings.MaxExecutionTimeoutSeconds, ApiSettings.ResolveTimeoutSeconds(ApiSettings.MaxExecutionTimeoutSeconds + 1000));

    [TestMethod]
    public void ResolveMinimumFrames_NoOverride_ReturnsConfiguredDefault() =>
        Assert.AreEqual(ApiSettings.DefaultMinimumFrames, ApiSettings.ResolveMinimumFrames(null));

    [TestMethod]
    public void ResolveMinimumFrames_OverrideProvided_ReturnsOverride() =>
        Assert.AreEqual(5, ApiSettings.ResolveMinimumFrames(5));
}
