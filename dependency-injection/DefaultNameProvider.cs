using Microsoft.Extensions.Options;

namespace dependency_injection
{
    internal class DefaultNameProvider : INameProvider
    {
        private readonly Configuration configuration;

        public DefaultNameProvider(IOptions<Configuration> options)
        {
            this.configuration = options.Value;
        }

        public string Name => configuration.SomeSettings.Name;
    }
}