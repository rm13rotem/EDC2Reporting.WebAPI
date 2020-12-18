using System;

namespace Components
{
    public class DateComponent : AbstractComponent
    {
        private DateTime _myValue;
        public DateTime MyValue
        {
            get { return _myValue.Date; }
            set { _myValue = DateTime.Parse(value.ToLongDateString()); }
        }

        public DateComponent(string label, string name) : base(label, name)
        {
            DefaultValue = DateTime.Now.ToLongDateString();
            _myValue = DateTime.Now;
            SerializedValue = DefaultValue;
        }
    }
}
