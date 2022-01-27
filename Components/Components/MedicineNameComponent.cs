using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Components
{
    public class MedicineNameComponent : AbstractComponent
    {
        public MedicineNameComponent(string label, string name) : base(label, name)
        {

        }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }

}
