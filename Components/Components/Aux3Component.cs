using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Components
{
    public class Aux3Component : AbstractComponent
    {
        public Aux3Component(string label, string name) : base(label, name)
        {

        }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }

}
