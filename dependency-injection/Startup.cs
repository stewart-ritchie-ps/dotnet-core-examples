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
            // Add settings from a couple of Json files in the project (these are set to 'Copy Always' on build).
            // Add user secrets (if these exist), remember UserSecretsId in project file.
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configuration/SomeSettings.json", optional: false)
                .AddJsonFile("Configuration/MoreSettings.json", optional: false)
                .AddUserSecrets<Configuration>()
                .Build();

            // Load configuration type and add support for IOptions<Configuration>.
            // Add some other services.
            Services = new ServiceCollection()
                .Configure<Configuration>(Configuration.GetSection(nameof(Configuration)))
                .AddOptions()
                .AddSingleton<Greeter>()                                // Concrete type
                .AddSingleton<INameProvider, DefaultNameProvider>()     // Concrete type for Interface
                .AddSingleton<Job.WriteLine>(Console.WriteLine)         // Method group for Delegate
                .AddSingleton<Job>();
        }

        // Build service provider.
        public ServiceProvider Build() => Services.BuildServiceProvider();

        public Startup WithName(string name)
        {
            // Override default name provider in dependecy injection.
            // Must be called before Build().
            Services.AddSingleton<INameProvider>(new NameProvider(name));
            return this;
        }
    }
}
