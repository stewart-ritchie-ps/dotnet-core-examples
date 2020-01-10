using Example.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

namespace Example
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;
        private readonly IServiceCollection Services;

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("SwitchConfig.json", optional: false)
                .Build();

            Services = new ServiceCollection()
                .Configure<Configuration>(Configuration.GetSection(nameof(Configuration)))
                .AddOptions()
                .AddSingleton<IEnumerable<Switch>>(_ =>
                {
                    var config = _.GetService<IOptions<Configuration>>().Value;

                    return new Switch[]
                    {
                        Switches.From<SupportComments>(config, "comments"),
                        Switches.From<SupportTopics>(config, "topics")
                    };
                })
                .AddSingleton<AllowTopicComments>();
        }

        public ServiceProvider Build()
        {
            return Services.BuildServiceProvider();
        }
    }
}
