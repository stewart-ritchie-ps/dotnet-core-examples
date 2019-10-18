using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace dependency_injection
{
    internal class Startup
    {
        private readonly IConfigurationRoot Configuration;
        private readonly IServiceCollection Services;

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configuration/SomeSettings.json", optional: false)
                .AddJsonFile("Configuration/MoreSettings.json", optional: false)
                .AddUserSecrets<Configuration>()
                .Build();

            Services = new ServiceCollection()
                .Configure<Configuration>(Configuration.GetSection(nameof(Configuration)))
                .AddOptions()
                .AddSingleton<Greeter>()
                .AddSingleton<INameProvider, DefaultNameProvider>()
                .AddSingleton<Job.WriteLine>(Console.WriteLine)
                .AddSingleton<Job>();
        }

        public ServiceProvider Build() => Services.BuildServiceProvider();

        public Startup WithName(string name)
        {
            Services.AddSingleton<INameProvider>(new NameProvider(name));
            return this;
        }
    }
}
