namespace SportsBetting.Handlers.Queries.Common.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntitiesByIdQuery<TEntity> : IQuery<IEnumerable<TEntity>>
        where TEntity : BaseModel
    {
        public EntitiesByIdQuery(IEnumerable<string> ids)
        {
            Ids = ids;
        }

        public EntitiesByIdQuery(IEnumerable<string> ids, Expression<Func<TEntity, bool>> expression)
        {
            Ids = ids;
            Expression = expression;
        }

        public IEnumerable<string> Ids { get; set; }

        public Expression<Func<TEntity, bool>> Expression { get; set; }
    }
}