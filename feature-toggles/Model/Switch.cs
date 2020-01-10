using System.Diagnostics;

namespace Example.Model
{
    /// <summary>
    /// Model for a switch.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebugText) + ",nv}")]
    public abstract class Switch
    {
        /// <summary>
        /// Unknown switch.
        /// </summary>
        public static Switch Unknown = new NullSwitch();

        public abstract string DebugText { get; }

        public abstract bool Check(object state);

        /// <summary>
        /// A non-functional switch - Check() always returns false.
        /// </summary>
        private class NullSwitch : Switch
        {
            public override string DebugText => $"{nameof(NullSwitch)}";

            public override bool Check(object state) => false;
        }
    }
}