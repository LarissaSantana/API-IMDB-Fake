using IMDb.Domain.Core.Messages;
using MediatR;
using System.Threading.Tasks;

namespace IMDb.Domain.Core.Bus
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        public Task<bool> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
