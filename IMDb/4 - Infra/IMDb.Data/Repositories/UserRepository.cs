using IMDb.Data.Context;
using IMDb.Data.Core;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;

namespace IMDb.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMDbContext _context;
        public UserRepository(IMDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
