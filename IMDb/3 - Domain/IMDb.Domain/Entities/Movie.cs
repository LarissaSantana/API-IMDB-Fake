﻿using FluentValidation;
using IMDb.Domain.Commands;
using IMDb.Domain.DomainObjects;
using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDb.Domain.Entities
{
    public class Movie : BaseEntity<Movie>
    {
        public string Title { get; private set; }
        public Genre Genre { get; private set; }
        public float? Mean { get; private set; }
        public virtual ICollection<CastOfMovie> CastOfMovies { get; private set; }
        public virtual ICollection<RatingOfMovie> RatingOfMovies { get; private set; }

        protected Movie()
        {
            CastOfMovies = new List<CastOfMovie>();
            RatingOfMovies = new List<RatingOfMovie>();
        }

        public Movie(Guid id, string title, Genre genre)
        {
            Id = id;
            Title = title;
            Genre = genre;
        }

        public override bool IsValid()
        {
            if (CastOfMovies.Any() && CastOfMovies.Any(x => x.Cast != null))
            {
                RuleFor(movie => movie.CastOfMovies)
                    .Must(castOfMovies => castOfMovies.Any(x => x.Cast.CastType == CastType.Director))
                    .WithMessage("The movie must have at least one director!");

                if (Genre != Genre.Animation)
                {
                    RuleFor(movie => movie.CastOfMovies)
                        .Must(castOfMovies => castOfMovies.Any(x => x.Cast.CastType == CastType.Actor))
                        .WithMessage("The movie must have at least one actor!");
                }
            }

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void AddCastOfMovie(ICollection<CastOfMovie> castOfMovie)
        {
            castOfMovie.ToList().ForEach(c => CastOfMovies.Add(c));
        }

        public void AddMean(float mean)
        {
            Mean = mean;
        }

        public static class MovieFactory
        {
            public static Movie Create(Genre genre, string title, Guid? id = null)
            {
                var movie = new Movie
                {
                    Id = id ?? Guid.NewGuid(),
                    Genre = genre,
                    Title = title
                };

                return movie;
            }
        }
    }
}
