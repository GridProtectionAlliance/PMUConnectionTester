using System;
using System.IO;
using System.Net;
using System.Web.Http.Results;
using ConnectionTester.Api.Controllers;
using ConnectionTester.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMUConnectionTester.Api.Tests.Controllers;

[TestClass]
public class PmuConnectionTesterControllerTests
{
    private static readonly string SampleCapturePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Sample1344.PmuCapture");

    private static PmuConnectionTesterController NewController() => new();

    [TestMethod]
    public void File_NullRequest_ReturnsBadRequestWithFailStatus()
    {
        PmuConnectionTesterController controller = NewController();

        NegotiatedContentResult<TestResponse> result = controller.File(null) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.AreEqual("FAIL", result.Content.OverallStatus);
    }

    [TestMethod]
    public void File_InvalidModelState_ReturnsBadRequest()
    {
        PmuConnectionTesterController controller = NewController();
        controller.ModelState.AddModelError(nameof(FileTestRequest.Filename), "Required");

        NegotiatedContentResult<TestResponse> result = controller.File(new FileTestRequest()) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [TestMethod]
    public void File_InvalidProtocol_ReturnsBadRequestMentioningProtocol()
    {
        PmuConnectionTesterController controller = NewController();
        FileTestRequest request = new() { Filename = SampleCapturePath, Protocol = "NotAProtocol" };

        NegotiatedContentResult<TestResponse> result = controller.File(request) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        StringAssert.Contains(result.Content.Message, "NotAProtocol");
    }

    [TestMethod]
    public void File_FileNotFound_ReturnsOkWithFailStatus()
    {
        PmuConnectionTesterController controller = NewController();
        FileTestRequest request = new() { Filename = @"C:\does\not\exist.PmuCapture", Protocol = "IEEE1344" };

        OkNegotiatedContentResult<TestResponse> result = controller.File(request) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("FAIL", result.Content.OverallStatus);
        StringAssert.Contains(result.Content.Message, "File not found");
    }

    [TestMethod]
    public void File_ValidSampleCapture_PassesRealPlayback()
    {
        Assert.IsTrue(System.IO.File.Exists(SampleCapturePath), $"Test capture file not found at {SampleCapturePath}");

        PmuConnectionTesterController controller = NewController();
        FileTestRequest request = new()
        {
            Filename = SampleCapturePath,
            Protocol = "IEEE1344",
            AutoRepeat = false,
            MinimumFrames = 3,
            TimeoutSeconds = 15
        };

        OkNegotiatedContentResult<TestResponse> result = controller.File(request) as OkNegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual("PASS", result.Content.OverallStatus);
        Assert.IsTrue(result.Content.Data.DataFramesReceived >= 3);
    }

    [TestMethod]
    public void Tcp_InvalidModelState_ReturnsBadRequest()
    {
        PmuConnectionTesterController controller = NewController();
        controller.ModelState.AddModelError(nameof(TcpTestRequest.Host), "Required");

        NegotiatedContentResult<TestResponse> result = controller.Tcp(new TcpTestRequest()) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [TestMethod]
    public void Tcp_InvalidProtocol_ReturnsBadRequestMentioningProtocol()
    {
        PmuConnectionTesterController controller = NewController();
        TcpTestRequest request = new() { Host = "localhost", Port = 4712, Protocol = "NotAProtocol" };

        NegotiatedContentResult<TestResponse> result = controller.Tcp(request) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        StringAssert.Contains(result.Content.Message, "NotAProtocol");
    }

    [TestMethod]
    public void Udp_InvalidModelState_ReturnsBadRequest()
    {
        PmuConnectionTesterController controller = NewController();
        controller.ModelState.AddModelError(nameof(UdpTestRequest.Host), "Required");

        NegotiatedContentResult<TestResponse> result = controller.Udp(new UdpTestRequest()) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [TestMethod]
    public void Udp_InvalidProtocol_ReturnsBadRequestMentioningProtocol()
    {
        PmuConnectionTesterController controller = NewController();
        UdpTestRequest request = new() { Host = "localhost", LocalPort = 4712, RemotePort = 4713, Protocol = "NotAProtocol" };

        NegotiatedContentResult<TestResponse> result = controller.Udp(request) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        StringAssert.Contains(result.Content.Message, "NotAProtocol");
    }

    [TestMethod]
    public void UdpPassive_InvalidModelState_ReturnsBadRequest()
    {
        PmuConnectionTesterController controller = NewController();
        controller.ModelState.AddModelError(nameof(UdpPassiveTestRequest.LocalPort), "Required");

        NegotiatedContentResult<TestResponse> result = controller.UdpPassive(new UdpPassiveTestRequest()) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [TestMethod]
    public void UdpPassive_InvalidProtocol_ReturnsBadRequestMentioningProtocol()
    {
        PmuConnectionTesterController controller = NewController();
        UdpPassiveTestRequest request = new() { LocalPort = 4712, Protocol = "NotAProtocol" };

        NegotiatedContentResult<TestResponse> result = controller.UdpPassive(request) as NegotiatedContentResult<TestResponse>;

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        StringAssert.Contains(result.Content.Message, "NotAProtocol");
    }
}
