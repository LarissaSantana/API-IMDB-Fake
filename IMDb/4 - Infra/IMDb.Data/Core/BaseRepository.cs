using IMDb.Data.Context;
using IMDb.Domain.Core.Data;
using IMDb.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IMDb.Data.Core
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity<TEntity>
    {
        private readonly IMDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(IMDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            Add<TEntity>(entity);
        }

        protected void Add<T>(T entity) where T : BaseEntity<T>
        {
            _context.Add(entity);
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>()
              .AsNoTracking()
              .AsEnumerable();
        }

        public IEnumerable<TEntity> GetByFilters(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include)
        {
            return GetByFilters<TEntity>(predicate, include);
        }

        protected IEnumerable<T> GetByFilters<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include) where T : BaseEntity<T>
        {
            if (predicate == null) return null;

            IQueryable<T> query;
            query = _context.Set<T>().AsNoTracking();

            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }

            return query.Where(predicate).AsEnumerable();
        }

        public TEntity GetById(Guid id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
