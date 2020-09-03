using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Interfaces;
using NHibernate;

namespace ORMNhibernate
{
    public interface INhRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity { }
    
    public class NhRepository<TEntity> : INhRepository<TEntity> where TEntity : class, IEntity
    {
        private IUnitOfWork<ISession> _unitOfWork;
        
        public NhRepository(IUnitOfWork<ISession> pUnitOfWorkOfWork)
        {
            _unitOfWork = pUnitOfWorkOfWork;
        }

        public TEntity Get(int id)
        {
            return _unitOfWork.Context.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.Context.Query<TEntity>();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> exp)
        {
            return _unitOfWork.Context.Query<TEntity>().Where(exp);
        }

        public void Save(TEntity entity)
        {
            _unitOfWork.Context.Save(entity);
        }

        public void Update(TEntity entity)
        {
            _unitOfWork.Context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _unitOfWork.Context.Delete(entity);
        }
    }
}