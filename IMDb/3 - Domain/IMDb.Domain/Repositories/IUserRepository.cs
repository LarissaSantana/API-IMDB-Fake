using IMDb.Domain.Core.Data;
using IMDb.Domain.Entities;
using IMDb.Domain.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IMDb.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
       IEnumerable<Role> GetRole(Expression<Func<Role, bool>> predicate);
       Pagination<User> GetUsersWithPagination(Expression<Func<User, bool>> predicate, int pageNumber, int pageSize);
    }
}
