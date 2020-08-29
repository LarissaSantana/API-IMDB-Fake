using IMDb.Domain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IMDb.Domain.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetByFilters(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include);
        IEnumerable<TEntity> GetAll();
        void Delete(Guid id);
    }
}
