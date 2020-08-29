using IMDb.Domain.DomainObjects;
using System;
using System.Collections.Generic;

namespace IMDb.Domain.Entities
{
    public class Role : BaseEntity<Role>
    {
        public string Name { get; private set; }
        public virtual ICollection<User> Users { get; private set; }

        protected Role()
        {
            Users = new List<User>();
        }

        public Role(Guid id, string name)
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
