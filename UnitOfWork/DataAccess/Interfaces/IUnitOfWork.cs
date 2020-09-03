using System;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable
    {
        void Commit();
        void Rollback();
        TContext Context { get; }
    }
}