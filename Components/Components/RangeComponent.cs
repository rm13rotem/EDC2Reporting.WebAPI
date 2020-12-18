using System;

namespace Components
{
    public class RangeComponent : AbstractComponent
    {
        public RangeValue MyValue { get; set; }
        public RangeComponent(string label, string name) : base (label, name)
        {
            MyValue = new RangeValue();
        }
    }
}
