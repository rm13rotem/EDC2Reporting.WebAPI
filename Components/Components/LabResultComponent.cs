using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Components
{
    public class LabResultComponent : AbstractComponent
    {
        public LabResultComponent(string label, string name) : base(label, name)
        {

        }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }

}
