using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> exp);
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}