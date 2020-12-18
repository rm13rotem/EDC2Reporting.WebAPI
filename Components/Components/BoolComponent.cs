using System;

namespace Components
{
    public class BoolComponent : AbstractComponent
    {
        public BoolComponent(string label, string name) : base(label, name)
        {
            DefaultValue = true.ToString();
            SerializedValue = true.ToString();
        }
    }
}
