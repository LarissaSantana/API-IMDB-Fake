using IMDb.Domain.Core.Data;
using IMDb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IMDb.Domain.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        //Cast
        public void AddCast(Cast cast);

        //CastOfMovie


        //RatingOfMovie
        void AddRatingOfMovie(RatingOfMovie ratingOfMovie);
        void UpdateRatingOfMovie(RatingOfMovie ratingOfMovie);
        IEnumerable<RatingOfMovie> GetRatingOfMoviesByFilters(Expression<Func<RatingOfMovie, bool>> predicate,
           params Expression<Func<RatingOfMovie, object>>[] include);
    }
}
