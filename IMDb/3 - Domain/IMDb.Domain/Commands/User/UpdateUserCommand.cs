using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Commands.User
{
    public class UpdateUserCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }        

        public UpdateUserCommand(string name, string password, Guid id)
        {
            Name = name;            
            Id = id;
        }
    }
}
