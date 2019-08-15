namespace SportsBetting.Handlers.Queries.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntitiesByKeyQuery<TEntity> : IQuery<IEnumerable<TEntity>>
        where TEntity : BaseModel
    {
        public EntitiesByKeyQuery(IEnumerable<int> keys)
        {
            Keys = keys;
        }

        public EntitiesByKeyQuery(IEnumerable<int> keys, Expression<Func<TEntity, bool>> expression)
        {
            Keys = keys;
            Expression = expression;
        }

        public IEnumerable<int> Keys { get; set; }

        public Expression<Func<TEntity, bool>> Expression { get; set; }
    }
}