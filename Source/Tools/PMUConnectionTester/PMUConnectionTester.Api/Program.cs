using System;
using ConnectionTester.Api.Infrastructure;
using Microsoft.Owin.Hosting;

namespace ConnectionTester.Api;

internal static class Program
{
    private static void Main()
    {
        string url = $"http://localhost:{ApiSettings.ListenPort}/";

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
