using ConnectionTester.Api.Infrastructure;
using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace ConnectionTester.Api;

/// <summary>
/// Hosts the OWIN self-hosted Web API under the Windows Service Control Manager so the API starts
/// automatically with the host machine, independent of any interactive user session.
/// </summary>
internal sealed class ApiWindowsService : ServiceBase
{
    public const string ServiceId = "PMUConnectionTesterApi";

    private IDisposable m_webApp;

    public ApiWindowsService()
    {
        ServiceName = ServiceId;
    }

    protected override void OnStart(string[] args)
    {
        string url = $"http://localhost:{ApiSettings.ListenPort}/";

        m_webApp = WebApp.Start<Startup>(url);
    }

    protected override void OnStop()
    {
        m_webApp?.Dispose();
        m_webApp = null;
    }
}