﻿using DataServices.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Components
{
    public class AbstractComponent : IValidatableObject, IPersistentEntity
    {
        public ComponentType ComponentType { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }
        public string Label { get; set; }

        public AbstractComponent()
        {

        }

        public AbstractComponent(string label, string name)
        {
            Label = label;
            this.Name = name;
        }

        public int OrderId { get; set; } // seriale order inside a module/form 1,2,3...
        public virtual string SerializedValue { get; set; }
        public virtual string DefaultValue { get; set; }
        
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Label))
                yield return new ValidationResult($"{nameof(Label)} cannot be null, empty or blank - that makes no sense for a component",
                    new string[] { nameof(Label) });
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult($"{nameof(Name)} cannot be null, empty or blank - that makes no sense for a component",
                    new string[] { nameof(Name) });
            if (OrderId == 0)
                yield return new ValidationResult($"{nameof(OrderId)} must be 1, 2, 3 ...",
                    new string[] { nameof(Label) });
        }
    }
}