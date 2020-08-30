using FluentValidation;
using IMDb.Domain.DomainObjects;
using System;

namespace IMDb.Domain.Entities
{
    public class RatingOfMovie : BaseEntity<RatingOfMovie>
    {
        public int Rate { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }
        public Guid MovieId { get; private set; }
        public virtual Movie Movie { get; private set; }

        protected RatingOfMovie() { }

        public RatingOfMovie(Guid id, Guid userId, Guid movieId)
        {
            Id = id;
            UserId = userId;
            MovieId = movieId;
        }

        public override bool IsValid()
        {
            RuleFor(movieRating => movieRating.Rate)
                .InclusiveBetween(0, 4)
                .WithMessage("The \"Rate\" field must be between 0 and 4!");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void UpdateRate(int rate)
        {
            Rate = rate;
        }

        public static class RatingOfMovieFactory
        {
            public static RatingOfMovie Create(int rate, Guid movieId, Guid userId)
            {
                var ratingOfMovie = new RatingOfMovie
                {
                    Id = Guid.NewGuid(),
                    Rate = rate,
                    MovieId = movieId,
                    UserId = userId
                };
                return ratingOfMovie;
            }
        }
    }
}
