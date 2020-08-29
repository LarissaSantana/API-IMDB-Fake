using IMDb.Domain.DomainObjects;
using System;

namespace IMDb.Domain.Entities
{
    public class User : BaseEntity<User>
    {
        public string Name { get; private set; }
        public bool Status { get; private set; }
        public string Password { get; private set; }

        protected User() { }

        public User(Guid id, string name, bool status, string password)
        {
            Id = id;
            Name = name;
            Status = status;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
