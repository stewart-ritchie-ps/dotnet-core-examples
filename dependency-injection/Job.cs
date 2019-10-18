using System;

namespace dependency_injection
{
    public class Job
    {
        private readonly Greeter greeter;
        private readonly WriteLine writeLine;

        public delegate void WriteLine(string value);

        public Job(Greeter greeter, WriteLine writeLine)
        {
            this.greeter = greeter;
            this.writeLine = writeLine;
        }

        public void Run()
        {
            if (writeLine == null)
                throw new ArgumentNullException(nameof(writeLine));

            for (int i = 0; i < 3; i++)
            {
                writeLine(greeter.Greet());
            }
        }
    }
}
