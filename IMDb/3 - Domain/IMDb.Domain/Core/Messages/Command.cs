using IMDb.Domain.Core.Data;
using MediatR;
using System;

namespace IMDb.Domain.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
