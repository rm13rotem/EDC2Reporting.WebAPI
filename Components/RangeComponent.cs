using System;

namespace Components
{
    public class RangeComponent : AbstractComponent
    {
        public RangeValue MyValue { get; set; }
        public RangeComponent()
        {
            MyValue = new RangeValue();
        }
    }
}
