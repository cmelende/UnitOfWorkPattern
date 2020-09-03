using System;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace ORMEntityFramework
{

    public class EfUnitOfWork : IUnitOfWork<DbContext>
    {
        
        private IDbContextTransaction _transaction;
        public DbContext Context { get; protected set; }
        
        public EfUnitOfWork(DbContext context)
        {
            Context = context;
            _transaction = Context.Database.BeginTransaction();
        }
        
        public void Dispose()
        {
            try
            {
                Commit();
            }
            catch (Exception)
            {
                Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
                Context.Dispose();
                Context = null;
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

    }
}