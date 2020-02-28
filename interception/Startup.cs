using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using System.Reflection;

using static Example.Interception;

namespace Example
{
    public class Startup
    {
        private readonly IServiceCollection ServiceDescriptors;

        public Startup()
        {
            ServiceDescriptors = new ServiceCollection()
                .AddSingleton(_ => new HttpClient(new HttpClientHandler
                {
                    UseProxy = false
                }));

            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(Request)))
                .ToList()
                .ForEach(t => ServiceDescriptors.AddTransient(t, _ => Intercept<RequestInterceptor>(t, _)));
        }

        public ServiceProvider Build() => ServiceDescriptors.BuildServiceProvider();
    }
}
