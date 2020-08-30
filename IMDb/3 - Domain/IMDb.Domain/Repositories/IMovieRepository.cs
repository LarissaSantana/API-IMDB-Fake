using IMDb.Domain.Core.Data;
using IMDb.Domain.Entities;
using IMDb.Domain.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IMDb.Domain.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Movie GetMovieById(Guid id);
        IEnumerable<Movie> GetMovies(Expression<Func<Movie, bool>> predicate);
        Pagination<Movie> GetMoviesWithPagination(Expression<Func<Movie, bool>> predicate, int pageNumber, int pageSize);

        //Cast
        IEnumerable<Cast> GetCast(Expression<Func<Cast, bool>> predicate);
        public void AddCast(Cast cast);

        //CastOfMovie


        //RatingOfMovie
        void AddRatingOfMovie(RatingOfMovie ratingOfMovie);
        void UpdateRatingOfMovie(RatingOfMovie ratingOfMovie);
        IEnumerable<RatingOfMovie> GetRatingOfMoviesByFilters(Expression<Func<RatingOfMovie, bool>> predicate,
           params Expression<Func<RatingOfMovie, object>>[] include);
    }
}
