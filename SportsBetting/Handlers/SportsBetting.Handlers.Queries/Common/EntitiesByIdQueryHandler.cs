namespace SportsBetting.Handlers.Queries.Common
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntitiesByIdQueryHandler<TEntity> : IQueryHandler<EntitiesByIdQuery<TEntity>, IEnumerable<TEntity>>
        where TEntity : BaseModel
    {
        private readonly ICache<TEntity> cache;

        public EntitiesByIdQueryHandler(ICache<TEntity> cache)
        {
            this.cache = cache;
        }

        public IEnumerable<TEntity> Handle(EntitiesByIdQuery<TEntity> query)
        {
            IEnumerable<TEntity> entities = cache.All(x => query.Ids.Contains(x.Id));

            return entities;
        }
    }
}