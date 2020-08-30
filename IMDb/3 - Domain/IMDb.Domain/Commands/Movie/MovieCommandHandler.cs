using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Core.Messages;
using IMDb.Domain.Core.Notifications;
using IMDb.Domain.Entities;
using IMDb.Domain.Enums;
using IMDb.Domain.Events;
using IMDb.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static IMDb.Domain.Entities.Cast;
using static IMDb.Domain.Entities.CastOfMovie;
using static IMDb.Domain.Entities.Movie;
using static IMDb.Domain.Entities.RatingOfMovie;

namespace IMDb.Domain.Commands.Movie
{
    public class MovieCommandHandler : CommandHandler,
        IRequestHandler<AddMovieCommand, bool>,
        IRequestHandler<AddRatingOfMovieCommand, bool>,
        IRequestHandler<AddMeanCommand, bool>,
        IRequestHandler<AddCastCommand, bool>

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
            ValidateCastOfMovie(message);
            if (HasNotifications()) return false;

            var movie = MovieFactory.Create(message.Genre, message.Title);
            var castOfMovieList = new List<CastOfMovie>();

            foreach (var castId in message.CastIds)
            {
                var castOfMovieModel = CastOfMovieFactory.Create(movie.Id, castId);
                if (!castOfMovieModel.IsValid())
                    NotifyValidationErrors(castOfMovieModel.ValidationResult);

                castOfMovieList.Add(castOfMovieModel);
            }

            movie.AddCastOfMovie(castOfMovieList);
            if (!movie.IsValid())
                NotifyValidationErrors(movie.ValidationResult);

            _movieRepository.Add(movie);

            return await Task.FromResult(Commit());
        }

        private void ValidateCastOfMovie(AddMovieCommand message)
        {
            var casts = _movieRepository.GetCast(it => message.CastIds.Contains(it.Id)).ToList();
            var castsNotFound = message.CastIds.Except(casts.Select(x => x.Id));
            if (castsNotFound.Any())
                castsNotFound.ToList().ForEach(x => NotifyValidationErrors($"Cast not found. Id: {x}"));

            if (casts.Any())
            {
                if (!casts.Any(x => x.CastType == CastType.Director))
                    NotifyValidationErrors("The movie must have at least one director!");

                if (message.Genre != Genre.Animation && !casts.Any(x => x.CastType == CastType.Actor))
                    NotifyValidationErrors("The movie must have at least one actor!");
            }
        }

        public async Task<bool> Handle(AddCastCommand message, CancellationToken cancellationToken)
        {
            var cast = CastFactory.Create(message.Name, message.CastType);
            if (!cast.IsValid())
                NotifyValidationErrors(cast.ValidationResult);

            _movieRepository.AddCast(cast);

            return await Task.FromResult(Commit());
        }

        public async Task<bool> Handle(AddRatingOfMovieCommand message, CancellationToken cancellationToken)
        {
            var userAuthenticatedId = _movieRepository.GetUserAuthenticatedId();
            if (!userAuthenticatedId.HasValue || userAuthenticatedId == Guid.Empty)
            {
                NotifyValidationErrors("Invalid user");
                return false;
            }

            var ratingOfMovieRepo = _movieRepository.GetRatingOfMoviesByFilters(mr =>
                    mr.UserId == userAuthenticatedId.Value && 
                    mr.MovieId == message.MovieId).FirstOrDefault();

            RatingOfMovieAddedEvent ratingOfMovieAddedEvent = null;

            if (ratingOfMovieRepo == null)
            {
                var ratingOfMovie = RatingOfMovieFactory
                    .Create(message.Rate, message.MovieId, userAuthenticatedId.Value);

                if (!ratingOfMovie.IsValid())
                {
                    NotifyValidationErrors(ratingOfMovie.ValidationResult);
                    return false;
                }

                _movieRepository.AddRatingOfMovie(ratingOfMovie);
                ratingOfMovieAddedEvent = new RatingOfMovieAddedEvent(ratingOfMovie.Id, ratingOfMovie.Rate, ratingOfMovie.MovieId);
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
                ratingOfMovieAddedEvent = new RatingOfMovieAddedEvent(ratingOfMovie.Id, ratingOfMovie.Rate, ratingOfMovie.MovieId);
            }

            if (Commit())
            {
                if (ratingOfMovieAddedEvent != null)
                    await _bus.RaiseEvent(ratingOfMovieAddedEvent);

                return await Task.FromResult(true);
            }

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

            var movieToUpdate = MovieFactory.Create(movie.Genre, movie.Title, movie.Id);
            movieToUpdate.AddMean((float)mean);

            _movieRepository.Update(movieToUpdate);

            return await Task.FromResult(Commit());
        }
    }
}
