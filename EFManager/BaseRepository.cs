using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Entities.Abstract;
using Interfaces;

namespace EFManager
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Identity
    {
        private readonly DbContext context;

        private readonly DbSet<T> set;

        public BaseRepository(DbContext ctx)
        {
            ctx.Configuration.ProxyCreationEnabled = false;
            context = ctx;
            set = context.Set<T>();
        }

        public T Get(int id)
        {
            return set.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return set;
        }

        public T Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            var result = set.Add(entity);
            context.SaveChanges();
            return result;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            set.Remove(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = Get(id);
            if (entity != default(T))
            {
                Delete(entity);
            }
            context.SaveChanges();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate,
           params Expression<Func<T, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);

            return entities.Where(predicate).AsEnumerable();
        }

        private IQueryable<T> IncludeProperties(params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(set, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}