using System;

namespace Components
{
    public class Aux2Component : AbstractComponent
    {
        public Aux2Component(string label, string name) : base(label, name)
        {

        }
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }

}
