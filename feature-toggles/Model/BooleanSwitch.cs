namespace Example.Model
{
    /// <summary>
    /// Base class for an On/Off 2-state switch.
    /// </summary>
    public abstract class BooleanSwitch : Switch
    {
        public static Position Off = new OffPosition();

        public static Position On = new OnPosition();

        protected BooleanSwitch() : this(false)
        {
        }

        protected BooleanSwitch(bool initialState)
        {
            State = initialState ? On : Off;
        }

        public Position State { get; private set; }

        public override string DebugText 
            => $"{GetType().Name}: {State.Value}";

        public override bool Check(object state) 
            => State.Value.Equals((state as Position)?.Value);

        public virtual BooleanSwitch TurnOff()
        {
            State = Off;
            return this;
        }

        public virtual BooleanSwitch TurnOn()
        {
            State = On;
            return this;
        }

        private class OffPosition : Position 
        {
            public override string Value => "Off";
        }

        private class OnPosition : Position 
        {
            public override string Value => "On";
        }
    }
}
