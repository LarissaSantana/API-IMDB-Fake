using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Commands.User
{
    public class ChangeStatusCommand : Command
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }

        public ChangeStatusCommand(Guid id, bool status)
        {
            Id = id;
            Status = status;
        }
    }
}
