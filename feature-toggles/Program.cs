using System;
using Microsoft.Extensions.DependencyInjection;

namespace Example
{
    class Program
    {
        private static readonly Startup Startup = new Startup();

        static void Main(string[] args)
        {
            var allowTopicComments = Startup.Build()
                .GetService<AllowTopicComments>();

            Console.WriteLine(allowTopicComments.Decide());
        }
    }
}
