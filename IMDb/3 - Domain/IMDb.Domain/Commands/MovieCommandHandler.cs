using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Core.Messages;
using IMDb.Domain.Core.Notifications;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Domain.Commands
{
    public class MovieCommandHandler : CommandHandler,
        IRequestHandler<AddMovieCommand, bool>,
        IRequestHandler<AddRatingOfMovieCommand, bool>,
        IRequestHandler<AddMeanCommand, bool>
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

        public async Task<bool> Handle(AddRatingOfMovieCommand message, CancellationToken cancellationToken)
        {
            var ratingOfMovieRepo = _movieRepository.GetRatingOfMoviesByFilters(mr =>
                    mr.UserId == message.UserId && mr.MovieId == mr.MovieId).FirstOrDefault();

            if (ratingOfMovieRepo == null)
            {
                var ratingOfMovie = RatingOfMovie.RatingOfMovieFactory
                    .Create(message.Rate, message.MovieId, message.UserId);

                if (!ratingOfMovie.IsValid())
                {
                    NotifyValidationErrors(ratingOfMovie.ValidationResult);
                    return false;
                }

                _movieRepository.AddRatingOfMovie(ratingOfMovie);
                //TODO: Adicionar evento para calcular média
            }
            else
            {
                var ratingOfMovie = ratingOfMovieRepo;
                ratingOfMovie.UpdateRate(message.Rate);

                if (!ratingOfMovie.IsValid())
                {
                    NotifyValidationErrors(ratingOfMovie.ValidationResult);
                    return false;
                }

                _movieRepository.UpdateRatingOfMovie(ratingOfMovie);
                //TODO: Adicionar evento para calcular média
            }

            if (Commit()) return await Task.FromResult(true);

            return false;
        }

        public async Task<bool> Handle(AddMeanCommand message, CancellationToken cancellationToken)
        {
            var ratingOfMovie = _movieRepository.GetRatingOfMoviesByFilters(m => m.MovieId == message.MovieId,
                include => include.Movie).ToList();

            if (!ratingOfMovie.Any()) return false;

            var mean = ratingOfMovie.Select(x => x.Rate).Average();
            var movie = ratingOfMovie.Select(x => x.Movie).FirstOrDefault();

            if (movie == null) return false;
            movie.AddMean((float)mean);

            _movieRepository.Update(movie);

            if (Commit()) return await Task.FromResult(true);

            return false;
        }
    }
}
