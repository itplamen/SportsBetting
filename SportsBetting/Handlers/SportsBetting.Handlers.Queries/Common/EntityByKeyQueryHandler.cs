namespace SportsBetting.Handlers.Queries.Common
{
    using System.Linq;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntityByKeyQueryHandler<TEntity> : IQueryHandler<EntityByKeyQuery<TEntity>, TEntity>
        where TEntity : BaseModel
    {
        private readonly ICache<TEntity> cache;

        public EntityByKeyQueryHandler(ICache<TEntity> cache)
        {
            this.cache = cache;
        }

        public TEntity Handle(EntityByKeyQuery<TEntity> query)
        {
            TEntity entity = cache.All(x => x.Key == query.Key).FirstOrDefault();

            return entity;
        }
    }
}