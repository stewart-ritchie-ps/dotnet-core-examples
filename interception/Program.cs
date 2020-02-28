using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        private static readonly Startup Startup = new Startup();
        private static readonly Stopwatch stopwatch = new Stopwatch();

        static async Task Main(string[] args)
        {
            var services = Startup.Build();

            var http = services.GetService<HttpClient>();

            // Warming up the http client means we don't see the overhead when we test interception
            // Type created directly doesn't get intercepted.
            stopwatch.Restart();
            await new MyFirstRequest(http).Get();
            stopwatch.Stop();

            Console.WriteLine($"First request took {stopwatch.ElapsedMilliseconds} milliseconds (this include a lot of time to warm up HttpClient).");

            // Types from DI container get wrapped in interceptors.
            var requests = new Request[]
            {
                services.GetService<MyFirstRequest>(),
                services.GetService<MySecondRequest>(),
                services.GetService<MyFirstRequest>(),
                services.GetService<MySecondRequest>(),
                services.GetService<MyFirstRequest>(),
                services.GetService<MySecondRequest>(),
            };

            Console.WriteLine("Running the intercepted requests...");

            await Task.WhenAll(requests.Select(r => r.Get()));
                       
            Console.WriteLine("Complete");
        }
    }
}
