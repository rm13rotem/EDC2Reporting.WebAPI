using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Components
{
    public class BoolComponent : AbstractComponent, IValidatableObject
    {
        public BoolComponent(string label, string name) : base(label, name)
        {
            DefaultValue = true.ToString();
            SerializedValue = true.ToString();
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var BaseErrors = base.Validate(validationContext);
            List<ValidationResult> errors;
            if (BaseErrors == null)
                errors = new List<ValidationResult>();
            else
                    errors = BaseErrors.ToList();
            if (!bool.TryParse(SerializedValue, out bool result))
                errors.Add(new ValidationResult($"{nameof(SerializedValue)} cannot be anything other than true or false in a {this.GetType().Name}",
                    new string[] { nameof(SerializedValue) }));
            return errors;
        }
    }
}
