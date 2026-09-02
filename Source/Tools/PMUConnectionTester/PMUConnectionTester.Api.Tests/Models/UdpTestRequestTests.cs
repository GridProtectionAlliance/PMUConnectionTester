using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConnectionTester.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMUConnectionTester.Api.Tests.Models;

[TestClass]
public class UdpTestRequestTests
{
    private static List<ValidationResult> Validate(UdpTestRequest request)
    {
        List<ValidationResult> results = new();
        Validator.TryValidateObject(request, new ValidationContext(request), results, true);
        return results;
    }

    [TestMethod]
    public void Validate_MissingHostAndProtocol_ReturnsErrorsForBoth()
    {
        UdpTestRequest request = new() { LocalPort = 4712, RemotePort = 4713 };

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(2, results.Count);
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(UdpTestRequest.Host))));
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(UdpTestRequest.Protocol))));
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(65536)]
    public void Validate_LocalPortOutOfRange_ReturnsError(int port)
    {
        UdpTestRequest request = new()
        {
            Host = "localhost",
            Protocol = "IEEE1344",
            LocalPort = port,
            RemotePort = 4713
        };

        List<ValidationResult> results = Validate(request);

        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(UdpTestRequest.LocalPort))));
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(65536)]
    public void Validate_RemotePortOutOfRange_ReturnsError(int port)
    {
        UdpTestRequest request = new()
        {
            Host = "localhost",
            Protocol = "IEEE1344",
            LocalPort = 4712,
            RemotePort = port
        };

        List<ValidationResult> results = Validate(request);

        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(UdpTestRequest.RemotePort))));
    }

    [TestMethod]
    public void Validate_ValidRequest_HasNoErrors()
    {
        UdpTestRequest request = new()
        {
            Host = "localhost",
            Protocol = "IEEE1344",
            LocalPort = 4712,
            RemotePort = 4713
        };

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(0, results.Count);
    }
}
