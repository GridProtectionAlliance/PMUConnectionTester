using ConnectionTester.Api.Infrastructure;
using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace ConnectionTester.Api;

internal static class Program
{
    private static void Main()
    {
        // When launched by the Service Control Manager there is no interactive session - run as a
        // Windows service instead of the console loop below.
        if (!Environment.UserInteractive)
        {
            ServiceBase.Run(new ApiWindowsService());
            return;
        }

        string url = $"http://{ApiSettings.ListenAddress}:{ApiSettings.ListenPort}/";

        using (WebApp.Start<Startup>(url))
        {
            Console.WriteLine("PMU Connection Tester API");
            Console.WriteLine($"Listening on {url}");
            Console.WriteLine($"Swagger UI: {url}swagger");
            Console.WriteLine("Press Ctrl+C to exit...");

            System.Threading.ManualResetEvent exitSignal = new(false);

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                exitSignal.Set();
            };

            exitSignal.WaitOne();
        }
    }
}