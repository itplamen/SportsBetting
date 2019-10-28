namespace SportsBetting.Handlers.Queries.Common.QueryHandlers
{
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Common.Queries;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntityByIdQueryHandler<TEntity> : IQueryHandler<EntityByIdQuery<TEntity>, TEntity>
        where TEntity : BaseModel
    {
        private readonly ISportsBettingDbContext dbContext;

        public EntityByIdQueryHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TEntity Handle(EntityByIdQuery<TEntity> query)
        {
            TEntity entity = dbContext.GetCollection<TEntity>()
                .Find(x => x.Id == query.Id)
                .FirstOrDefault();

            return entity;
        }
    }
}