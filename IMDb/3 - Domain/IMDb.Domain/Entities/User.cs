using IMDb.Domain.DomainObjects;
using System;
using System.Collections.Generic;

namespace IMDb.Domain.Entities
{
    public class User : BaseEntity<User>
    {
        public string Name { get; private set; }
        public bool Status { get; private set; }
        public string Password { get; private set; }
        public Guid RoleId { get; private set; }
        public virtual Role Role { get; private set; }
        public virtual ICollection<RatingOfMovie> RatingOfMovies { get; set; }

        protected User()
        {
            RatingOfMovies = new List<RatingOfMovie>();
        }

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
