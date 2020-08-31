using IMDb.Data.Context;
using IMDb.Domain.Core.Data;
using System;

namespace IMDb.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMDbContext _context;

        public UnitOfWork(IMDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
