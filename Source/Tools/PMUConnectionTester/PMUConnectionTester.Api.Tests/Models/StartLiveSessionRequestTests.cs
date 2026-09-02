using ConnectionTester.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PMUConnectionTester.Api.Tests.Models;

[TestClass]
public class StartLiveSessionRequestTests
{
    [TestMethod]
    public void OptionalFields_DefaultToNull()
    {
        StartLiveSessionRequest request = new();

        Assert.IsNull(request.FrameRate);
        Assert.IsNull(request.MinCommunicationWaitSeconds);
        Assert.IsNull(request.CaptureDurationSeconds);
    }

    [TestMethod]
    public void Validate_MissingRequiredFields_ReturnsErrorsForEach()
    {
        StartLiveSessionRequest request = new() { Port = 4712 };

        List<ValidationResult> results = Validate(request);

        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(StartLiveSessionRequest.TransportProtocol))));
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(StartLiveSessionRequest.Host))));
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(StartLiveSessionRequest.PhasorProtocol))));
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(65536)]
    public void Validate_PortOutOfRange_ReturnsError(int port)
    {
        StartLiveSessionRequest request = new()
        {
            TransportProtocol = "Tcp",
            Host = "10.20.30.40",
            PhasorProtocol = "IEEEC37_118V2",
            Port = port
        };

        List<ValidationResult> results = Validate(request);

        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(StartLiveSessionRequest.Port))));
    }

    [TestMethod]
    public void Validate_ValidRequest_HasNoErrors()
    {
        StartLiveSessionRequest request = new()
        {
            TransportProtocol = "Tcp",
            Host = "10.20.30.40",
            Port = 4712,
            PhasorProtocol = "IEEEC37_118V2"
        };

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(0, results.Count);
    }

    private static List<ValidationResult> Validate(StartLiveSessionRequest request)
    {
        List<ValidationResult> results = new();
        Validator.TryValidateObject(request, new ValidationContext(request), results, true);
        return results;
    }
}