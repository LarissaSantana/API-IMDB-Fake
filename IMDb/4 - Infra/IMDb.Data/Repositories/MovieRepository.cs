﻿using IMDb.Data.Context;
using IMDb.Data.Core;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IMDb.Data.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        private readonly IMDbContext _context;

        public MovieRepository(IMDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddCast(Cast cast)
        {
            Add<Cast>(cast);
        }

        public Movie GetMovieById(Guid id)
        {
            return _context.Set<Movie>()
                           .Where(x => x.Id.Equals(id))
                           .Include(x => x.CastOfMovies)
                               .ThenInclude(x => x.Cast)
                           .FirstOrDefault();
        }

        public void AddRatingOfMovie(RatingOfMovie ratingOfMovie)
        {
            Add<RatingOfMovie>(ratingOfMovie);
        }

        public void UpdateRatingOfMovie(RatingOfMovie ratingOfMovie)
        {
            Update<RatingOfMovie>(ratingOfMovie);
        }

        public IEnumerable<Movie> GetMovies(Expression<Func<Movie, bool>> predicate)
        {
            IQueryable<Movie> query;

            query = _context.Set<Movie>()
                            .Include(x => x.CastOfMovies)
                                .ThenInclude(x => x.Cast)
                            .Include(x => x.RatingOfMovies)
                            .AsNoTracking();

            return query.Where(predicate).ToList();
        }

        public IEnumerable<RatingOfMovie> GetRatingOfMoviesByFilters(Expression<Func<RatingOfMovie, bool>> predicate,
           params Expression<Func<RatingOfMovie, object>>[] include)
        {
            return GetByFilters<RatingOfMovie>(predicate, include);
        }
    }
}
