using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Core.Messages;
using IMDb.Domain.Core.Notifications;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Domain.Commands
{
    public class MovieCommandHandler : CommandHandler,
        IRequestHandler<AddMovieCommand, bool>
    {
        private readonly IMovieRepository _movieRepository;

        public MovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork uow, IMediatorHandler bus,
            IDomainNotificationHandler<DomainNotification> notifications)
            : base(bus, uow, notifications)
        {
            _movieRepository = movieRepository;
        }

        public async Task<bool> Handle(AddMovieCommand message, CancellationToken cancellationToken)
        {
            var movie = Movie.MovieFactory.Create(message.Genre, message.Title);
            var castOfMovieList = new List<CastOfMovie>();

            foreach (var cast in message.Cast)
            {
                var castModel = Cast.CastFactory.Create(cast.Name, cast.CastType);
                if (!castModel.IsValid())
                    NotifyValidationErrors(castModel.ValidationResult);

                var castOfMovieModel = CastOfMovie.CastOfMovieFactory.Create(movie.Id, castModel.Id);
                castOfMovieModel.AddCast(castModel);
                if (!castOfMovieModel.IsValid())
                    NotifyValidationErrors(castOfMovieModel.ValidationResult);

                castOfMovieList.Add(castOfMovieModel);
            }

            movie.AddCastOfMovie(castOfMovieList);
            if (!movie.IsValid())
                NotifyValidationErrors(movie.ValidationResult);

            _movieRepository.Add(movie);

            if (Commit()) return await Task.FromResult(true);

            return false;
        }

        //public async Task<bool> Handle(AddCastCommand message, CancellationToken cancellationToken)
        //{

        //    var cast = Cast.CastFactory.Create(message.Name, message.CastType);

        //    _movieRepository.AddCast(cast);

        //    return await Task.FromResult(_uow.Commit());
        //}
    }
}
