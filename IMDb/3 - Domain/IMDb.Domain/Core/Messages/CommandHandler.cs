using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;

namespace IMDb.Domain.Core.Messages
{
    public abstract class CommandHandler
    {
        protected readonly IMediatorHandler _bus;
        protected readonly IUnitOfWork _uow;

        public CommandHandler(IMediatorHandler bus, IUnitOfWork uow)
        {
            _bus = bus;
            _uow = uow;
        }
    }
}
