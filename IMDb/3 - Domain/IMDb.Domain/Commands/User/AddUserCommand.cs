using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Commands.User
{
    public class AddUserCommand : Command
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public Guid RoleId { get; private set; }
        public bool Status { get; private set; }

        public AddUserCommand(string name, string password, Guid roleId, bool status)
        {
            Name = name;
            Password = password;
            RoleId = roleId;
            Status = status;
        }
    }
}
