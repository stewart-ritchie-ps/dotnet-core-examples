namespace dependency_injection
{
    internal class NameProvider : INameProvider
    {
        public NameProvider(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}