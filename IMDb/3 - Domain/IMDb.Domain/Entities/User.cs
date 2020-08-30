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

        public void ChangeName(string name)
        {
            Name = name;
        }

        public User(Guid id, string name, bool status, string password, Guid roleId)
        {
            Id = id;
            Name = name;
            Status = status;
            Password = password;
            RoleId = roleId;
        }

        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public static class UserFactory
    {
        public static User Create(string name, Guid roleId, string password, Guid? id = null, bool status = true)
        {
            var user = new User
                (
                    id: id ?? Guid.NewGuid(),
                    name: name,
                    password: password,
                    status: status,
                    roleId: roleId
                );

            return user;
        }
    }
}
