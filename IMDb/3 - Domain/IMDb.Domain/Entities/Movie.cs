using IMDb.Domain.DomainObjects;
using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;

namespace IMDb.Domain.Entities
{
    public class Movie : BaseEntity<Movie>
    {
        public string Title { get; private set; }
        public Genre Genre { get; private set; }
        public float? Mean { get; private set; }
        public virtual ICollection<CastOfMovie> CastOfMovies { get; private set; }
        public virtual ICollection<RatingOfMovie> MovieRatings { get; private set; }

        protected Movie()
        {
            CastOfMovies = new List<CastOfMovie>();
            MovieRatings = new List<RatingOfMovie>();
        }

        public Movie(Guid id, string title, Genre genre)
        {
            Id = id;
            Title = title;
            Genre = genre;
        }

        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
