using Microsoft.Extensions.Options;

namespace dependency_injection
{
    public class Greeter
    {
        private readonly Configuration configuration;
        private readonly INameProvider nameProvider;

        public Greeter(IOptions<Configuration> options, INameProvider nameProvider)
        {
            configuration = options.Value;
            this.nameProvider = nameProvider;
        }

        public string Greet()
        {
            return $"{configuration.MoreSettings.Greeting} {nameProvider.Name}";
        }
    }
}
