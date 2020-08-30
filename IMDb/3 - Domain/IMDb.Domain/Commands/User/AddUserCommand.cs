using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Commands.User
{
    public class AddUserCommand : Command
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public bool Status { get; set; }

        public AddUserCommand(string name, string password, Guid roleId, bool status)
        {
            Name = name;
            Password = password;
            RoleId = roleId;
            Status = status;
        }
    }
}
