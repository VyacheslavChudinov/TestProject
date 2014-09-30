using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Entities.Abstract;

namespace Interfaces
{
    public interface IBaseRepository<T> where T : Identity
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

    }
}