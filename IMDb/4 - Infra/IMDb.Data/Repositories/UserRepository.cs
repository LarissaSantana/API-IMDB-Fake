using IMDb.Data.Context;
using IMDb.Data.Core;
using IMDb.Data.CrossCutting.Authorization;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using IMDb.Domain.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IMDb.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMDbContext _context;
        public UserRepository(IMDbContext context, AuthenticatedUser user) : base(context, user)
        {
            _context = context;
        }

        public IEnumerable<Role> GetRole(Expression<Func<Role, bool>> predicate)
        {
            return GetByFilters<Role>(predicate);
        }

        /// <summary>
        /// Returns a list of users ordered by name with pagination.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Pagination<User> GetUsersWithPagination(
            Expression<Func<User, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Set<User>()
                                .Include(x => x.Role)
                                .OrderBy(x => x.Name)
                                .AsNoTracking();

            var skipNumber = Pagination<User>.CalculateSkipNumber(pageNumber, pageSize);
            var totalItemCount = query.Where(predicate).Count();
            var users = query.Where(predicate).Skip(skipNumber).Take(pageSize).ToList();

            return new Pagination<User>
                (
                    items: users,
                    totalItemCount: totalItemCount,
                    pageSize: pageSize,
                    currentPage: pageNumber
                );
        }
    }
}
