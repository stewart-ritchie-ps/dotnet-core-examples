using System.Collections.Generic;
using System.Linq;

namespace Example.Model
{
    /// <summary>
    /// Model for a decision that is based on switches.
    /// </summary>
    public abstract class Decision
    {
        private readonly List<Switch> _switches = new List<Switch>();

        protected Decision(IEnumerable<Switch> switches) 
            => _switches.AddRange(switches);

        public IReadOnlyCollection<Switch> Switches 
            => _switches.AsReadOnly();

        public abstract bool Decide();

        protected bool Check<T>(object state) where T : Switch
            => GetSwitch<T>().Check(state);

        private Switch GetSwitch<T>() where T : Switch
            => Switches.OfType<T>().SingleOrDefault() ?? Switch.Unknown;
    }
}
