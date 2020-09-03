using System;
using DataAccess.Interfaces;
using FluentNHibernate;
using NHibernate;

namespace ORMNhibernate
{
    public class NhUnitOfWork : IUnitOfWork<ISession>
    {
        public ISession Context { get; set; }
        private ITransaction _transaction;
        private readonly ISessionSource _sessionSource;
        private bool TransactionActive => _transaction != null && _transaction.IsActive;
        
        public NhUnitOfWork(ISessionSource sessionSource)
        {
            _sessionSource = sessionSource;
            Context = _sessionSource.CreateSession();
            _transaction = Context.BeginTransaction();
        }
    
        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            if (TransactionActive)
            {
                _transaction.Rollback();
            }
        }


        public void Dispose()
        {
            try
            {
                _transaction.Commit();
            }
            catch(Exception e)
            {
                _transaction.Rollback();
                throw new TransactionException($"There was an error saving: {e.Message}");
            }
            finally
            {
                _transaction.Dispose();
                Context.Dispose();
                _transaction = null;
                Context = null;
            }
        }
    }
}