using Example.Model;
using System.Collections.Generic;

namespace Example
{
    public class Configuration : IConfigLookup
    {
        public IDictionary<string, string> Switches { get; set; }

        public string Lookup(string key)
        {
            return Switches[key];
        }
    }
}
