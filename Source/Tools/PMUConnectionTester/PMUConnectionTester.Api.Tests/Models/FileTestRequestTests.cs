using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConnectionTester.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMUConnectionTester.Api.Tests.Models;

[TestClass]
public class FileTestRequestTests
{
    private static List<ValidationResult> Validate(FileTestRequest request)
    {
        List<ValidationResult> results = new();
        Validator.TryValidateObject(request, new ValidationContext(request), results, true);
        return results;
    }

    [TestMethod]
    public void Validate_MissingFilenameAndProtocol_ReturnsErrorsForBoth()
    {
        FileTestRequest request = new();

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(2, results.Count);
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(FileTestRequest.Filename))));
        Assert.IsTrue(results.Exists(r => r.MemberNames.Contains(nameof(FileTestRequest.Protocol))));
    }

    [TestMethod]
    public void Validate_FilenameAndProtocolProvided_HasNoErrors()
    {
        FileTestRequest request = new()
        {
            Filename = "Sample1344.PmuCapture",
            Protocol = "IEEE1344"
        };

        List<ValidationResult> results = Validate(request);

        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    public void DefaultValues_MatchDocumentedDefaults()
    {
        FileTestRequest request = new();

        Assert.AreEqual(30, request.FrameRate);
        Assert.IsTrue(request.AutoRepeat);
        Assert.IsNull(request.MinimumFrames);
        Assert.IsNull(request.TimeoutSeconds);
    }
}
