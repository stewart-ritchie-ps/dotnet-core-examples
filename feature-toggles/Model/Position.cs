using System.Diagnostics;

namespace Example.Model
{
    /// <summary>
    /// Model for a switch position.
    /// </summary>
    [DebuggerDisplay(nameof(Value))]
    public abstract class Position
    {
        public abstract string Value { get; }
    }
}
