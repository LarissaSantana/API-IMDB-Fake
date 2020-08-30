using IMDb.Data.Context;
using IMDb.Data.Core;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IMDb.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMDbContext _context;
        public UserRepository(IMDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetRole(Expression<Func<Role, bool>> predicate)
        {
            return GetByFilters<Role>(predicate);
        }
    }
}
