using IMDb.Data.Context;
using IMDb.Data.Core;
using IMDb.Domain.Core.Pagination;
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
                           .AsNoTracking()
                           .FirstOrDefault();
        }

        public IEnumerable<Cast> GetCast(Expression<Func<Cast, bool>> predicate)
        {
            return GetByFilters<Cast>(predicate);
        }

        public void AddRatingOfMovie(RatingOfMovie ratingOfMovie)
        {
            Add<RatingOfMovie>(ratingOfMovie);
        }

        public void UpdateRatingOfMovie(RatingOfMovie ratingOfMovie)
        {
            Update<RatingOfMovie>(ratingOfMovie);
        }

        public Pagination<Movie> GetMoviesWithPagination(
            Expression<Func<Movie, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            IQueryable<Movie> query;

            query = _context.Set<Movie>()
                            .Include(x => x.CastOfMovies)
                                .ThenInclude(x => x.Cast)
                            .Include(x => x.RatingOfMovies)
                            .OrderByDescending(x => x.RatingOfMovies.Count())
                                .ThenBy(x => x.Title)
                            .AsNoTracking();

            var skipNumber = (pageNumber - 1) * pageSize;
            var totalItemCount = query.Where(predicate).Count();
            var movies = query.Where(predicate).Skip(skipNumber).Take(pageSize).ToList();

            return new Pagination<Movie>
                 (
                     items: movies,
                     totalItemCount: totalItemCount,
                     pageSize: pageSize,
                     currentPage: pageNumber
                 );
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
