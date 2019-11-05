using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace http_client_factory
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((context, builder) => 
                {
                    builder
                        .SetBasePath(context.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    services
                        .Configure<FastlyConfig>(context.Configuration.GetSection(nameof(FastlyConfig)))
                        .AddOptions()
                        .AddHttpClient()
                        .AddTransient<FastlyService>();
                })
                .UseConsoleLifetime()
                .Build();

            using var serviceScope = host.Services.CreateScope();

            var services = serviceScope.ServiceProvider;

            try
            {
                var fastlyService = services.GetRequiredService<FastlyService>();
                var stuff = await fastlyService.GetStats();

                Console.WriteLine(stuff);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }
    }
}
