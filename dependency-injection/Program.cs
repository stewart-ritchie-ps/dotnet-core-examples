using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace dependency_injection
{
    class Program
    {
        private static readonly Startup Startup;

        static Program()
        {
            Startup = new Startup();
        }

        static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "Dependency Injection Example"
            };

            app.HelpOption("--help|-h");

            var nameOption = app.Option("--name|-n", "Name", CommandOptionType.SingleValue);

            app.OnExecute(() => 
            { 
                if (nameOption.Value() != null)
                {
                    Startup.WithName(nameOption.Value());
                }
                
                Console.ForegroundColor = ConsoleColor.Green;

                Startup
                    .Build()
                    .GetService<Job>()
                    .Run();

                Console.ResetColor();

                return 0; 
            });

            try
            {
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
