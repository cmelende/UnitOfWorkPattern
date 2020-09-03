using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ORMEntityFramework
{

    public interface IEfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity { }
    public class EfRepository<TEntity> : IEfRepository<TEntity> where TEntity : class, IEntity
    {
        protected IUnitOfWork<DbContext> UnitOfWork;
        public EfRepository(IUnitOfWork<DbContext> pUnitOfWork)
        {
            UnitOfWork = pUnitOfWork;
        }
        public TEntity Get(int id)
        {
            return UnitOfWork.Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return UnitOfWork.Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> exp)
        {
            return UnitOfWork.Context.Set<TEntity>().Where(exp);
        }


        public void Save(TEntity entity)
        {
            UnitOfWork.Context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            TEntity current = Get(entity.Id);
            if (current == null) return;
            
            UnitOfWork.Context.Entry(entity).CurrentValues.SetValues(entity);
        }

        public void Delete(TEntity entity)
        {
            UnitOfWork.Context.Set<TEntity>().Remove(entity);
        }
    }
}