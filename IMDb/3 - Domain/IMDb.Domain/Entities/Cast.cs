using IMDb.Domain.DomainObjects;
using IMDb.Domain.Enums;
using System;

namespace IMDb.Domain.Entities
{
    public class Cast : BaseEntity<Cast>
    {
        public string Name { get; private set; }
        public CastType CastType { get; private set; }

        protected Cast() { }

        public Cast(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
