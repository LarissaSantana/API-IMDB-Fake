using System;

namespace IMDb.Domain.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
