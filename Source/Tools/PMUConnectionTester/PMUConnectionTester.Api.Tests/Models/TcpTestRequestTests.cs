using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConnectionTester.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMUConnectionTester.Api.Tests.Models;

[TestClass]
public class TcpTestRequestTests
{
    private static List<ValidationResult> Validate(TcpTestRequest request)
    {
        List<ValidationResult> results = new();
        Validator.TryValidateObject(request, new ValidationContext(request), results, true);
        return results;
    }

    [TestMethod]
    public void Validate_MissingHostAndProtocol_ReturnsErrorsForBoth()
    {
        TcpTestRequest request = new() { Port = 4712 };

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(2, results.Count);
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(TcpTestRequest.Host))));
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(TcpTestRequest.Protocol))));
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(65536)]
    public void Validate_PortOutOfRange_ReturnsError(int port)
    {
        TcpTestRequest request = new()
        {
            Host = "localhost",
            Protocol = "IEEE1344",
            Port = port
        };

        List<ValidationResult> results = Validate(request);

        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(TcpTestRequest.Port))));
    }

    [TestMethod]
    public void Validate_ValidRequest_HasNoErrors()
    {
        TcpTestRequest request = new()
        {
            Host = "localhost",
            Protocol = "IEEE1344",
            Port = 4712
        };

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    public void DefaultValues_MatchDocumentedDefaults()
    {
        TcpTestRequest request = new();

        Assert.AreEqual(30, request.FrameRate);
        Assert.IsFalse(request.IsListener);
    }
}
