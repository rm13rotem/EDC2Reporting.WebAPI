using System;

namespace Components
{
    public class TextComponent  : AbstractComponent
    {
        public TextComponent(string label, string name) : base (label, name)
        {
            
        }

        public TextComponent(string label, string name, string value) : base(label, name)
        {
            DefaultValue = value;
            SerializedValue = value;
        }
    }
}
