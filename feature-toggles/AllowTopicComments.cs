using Example.Model;
using System.Collections.Generic;

namespace Example
{
    /// <summary>
    /// Allow topic comments decision.
    /// </summary>
    public class AllowTopicComments : Decision
    {
        public AllowTopicComments(IEnumerable<Switch> switches) : base(switches)
        {
        }

        public override bool Decide()
        {
            return Check<SupportComments>(BooleanSwitch.On) 
                && Check<SupportTopics>(BooleanSwitch.On);
        }
    }
}
