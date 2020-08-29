using IMDb.Data.Context;
using IMDb.Data.Core;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;

namespace IMDb.Data.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        private readonly IMDbContext _context;

        public MovieRepository(IMDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
