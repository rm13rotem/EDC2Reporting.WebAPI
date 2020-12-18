using System;

namespace Components
{
    public class BoolComponent : AbstractComponent
    {
        public BoolComponent()
        {
            DefaultValue = true.ToString();
            Label = string.Empty;
            SerializedValue = true.ToString();
        }
    }
}
