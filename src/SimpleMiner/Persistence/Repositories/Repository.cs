using SimpleMiner.Persistence.Context;
using SimpleMiner.Persistence.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleMiner.Persistence.Repositories
{
    public class Repository : IRepository
    {
        protected DataBaseContext DbContext;

        public Repository(DataBaseContext dataBaseContext)
        {
            DbContext = dataBaseContext;
        }

        public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbContext.Set<TEntity>()
                .Where(predicate);
        }

        public IEnumerable<TEntity> Take<TEntity>(Expression<Func<TEntity, bool>> predicate, int take) where TEntity : class
        {
            return DbContext.Set<TEntity>()
                .Where(predicate)
                .Take(take)
                .AsNoTracking();
        }

        public virtual TEntity AddIfNotExists<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> predicate = null) where TEntity : class
        {
            if (!DbContext.Set<TEntity>().Any(predicate))
            {
                return DbContext.Set<TEntity>().Add(entity).Entity;
            }

            return DbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public int SaveChanges(int bacthSize)
        {
            try
            {
                return DbContext.SaveChanges(bacthSize);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public virtual TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return DbContext.Set<TEntity>()
                .Add(entity).Entity;
        }

        public IEnumerable<T> Query<T>(string query, params object[] parameters) where T : class
        {
            return DbContext.Set<T>().FromSqlRaw(query, parameters);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            var dbEntry = DbContext.Entry(entity);
            dbEntry.State = EntityState.Modified;
        }
    }
}
