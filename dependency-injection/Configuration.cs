namespace dependency_injection
{
    public class Configuration
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public SomeSettings SomeSettings { get; set; }

        public MoreSettings MoreSettings { get; set; }
    }

    public class SomeSettings
    {
        public string Name { get; set; }
    }

    public class MoreSettings
    {
        public string Greeting { get; set; }
    }
}
