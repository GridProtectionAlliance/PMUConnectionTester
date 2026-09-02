using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConnectionTester.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMUConnectionTester.Api.Tests.Models;

[TestClass]
public class UdpPassiveTestRequestTests
{
    private static List<ValidationResult> Validate(UdpPassiveTestRequest request)
    {
        List<ValidationResult> results = new();
        Validator.TryValidateObject(request, new ValidationContext(request), results, true);
        return results;
    }

    [TestMethod]
    public void Validate_MissingProtocol_ReturnsError()
    {
        UdpPassiveTestRequest request = new() { LocalPort = 4712 };

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(1, results.Count);
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(UdpPassiveTestRequest.Protocol))));
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(65536)]
    public void Validate_LocalPortOutOfRange_ReturnsError(int port)
    {
        UdpPassiveTestRequest request = new()
        {
            Protocol = "IEEE1344",
            LocalPort = port
        };

        List<ValidationResult> results = Validate(request);

        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(UdpPassiveTestRequest.LocalPort))));
    }

    [TestMethod]
    public void Validate_ValidRequest_HasNoErrors()
    {
        UdpPassiveTestRequest request = new()
        {
            Protocol = "IEEE1344",
            LocalPort = 4712
        };

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(0, results.Count);
    }
}
