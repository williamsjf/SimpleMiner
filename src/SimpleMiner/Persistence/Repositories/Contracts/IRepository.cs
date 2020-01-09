using SimpleMiner.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleMiner.Persistence.Repositories.Contracts
{
    public interface IRepository
    {
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        TEntity AddIfNotExists<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> predicate = null) where TEntity : class;
    }
}
