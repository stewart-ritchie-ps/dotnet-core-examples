using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace dependency_injection
{
    class Program
    {
        // Bootstrap the application.
        private static readonly Startup Startup = new Startup();

        static int Main(string[] args)
        {
            // Create a CLI with support for a --name option.
            var app = new CommandLineApplication
            {
                Name = "Dependency Injection Example"
            };

            app.HelpOption("--help|-h");

            var nameOption = app.Option("--name|-n", "Name", CommandOptionType.SingleValue);

            app.OnExecute(() => 
            { 
                // If the user passed --name in the CLI, we override the default strategy.
                if (nameOption.Value() != null)
                {
                    Startup.WithName(nameOption.Value());
                }
                
                Console.ForegroundColor = ConsoleColor.Green;

                // Get a Job service and run it.
                Startup
                    .Build()
                    .GetService<Job>()
                    .Run();

                Console.ResetColor();

                return 0; 
            });

            try
            {
                // Run the application...
                return app.Execute(args);
            }
            catch(CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to execute application: {ex.Message}");
                return 1;
            }
        }
    }
}
