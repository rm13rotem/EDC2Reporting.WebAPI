using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Components
{
    public class Aux1Component : AbstractComponent, IValidatableObject
    {
        public Aux1Component(string label, string name) : base (label, name)
        {

        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
}
