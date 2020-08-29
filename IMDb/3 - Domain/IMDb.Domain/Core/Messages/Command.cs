using IMDb.Domain.Core.Data;
using MediatR;
using System;

namespace IMDb.Domain.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        protected readonly IUnitOfWork _uow;
        public DateTime Timestamp { get; private set; }

        public Command(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
