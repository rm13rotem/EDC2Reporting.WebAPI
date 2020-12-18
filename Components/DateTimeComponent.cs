using System;

namespace Components
{
    public class DateTimeComponent : AbstractComponent
    {
        public DateTime MyValue{ get; set; }

        public override string SerializedValue { get => MyValue.ToString(); set {
                DateTime _myValue;
                 bool isSuccess = DateTime.TryParse( value, out _myValue);
                if (isSuccess)
                {
                    MyValue = _myValue;
                    return;
                }

                // else 
                if (MyValue == new DateTime() &&
                    DateTime.TryParse(DefaultValue, out _myValue))
                    MyValue = _myValue;

                // otherwise - do nothing;
            }
    }
        public DateTimeComponent(string label, string name) : base(label, name)
        {
            DefaultValue = DateTime.Now.ToLongDateString();

        }
}
