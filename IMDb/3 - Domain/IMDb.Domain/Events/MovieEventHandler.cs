using IMDb.Domain.Commands;
using IMDb.Domain.Core.Bus;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Domain.Events
{
    public class MovieEventHandler :
        INotificationHandler<RatingOfMovieAddedEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public MovieEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(RatingOfMovieAddedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new AddMeanCommand(message.MovieId));
        }
    }
}
