namespace SportsBetting.Handlers.Commands.Common
{
    using System;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Commands.Contracts;

    public class HideEntitiesCommandHandler<TEntity> : ICommandHandler<HideEntitiesCommand<TEntity>>
        where TEntity : BaseModel
    {
        private readonly ICache<TEntity> cache;
        private readonly ISportsBettingDbContext dbContext;

        public HideEntitiesCommandHandler(ICache<TEntity> cache, ISportsBettingDbContext dbContext)
        {
            this.cache = cache;
            this.dbContext = dbContext;
        }

        public void Handle(HideEntitiesCommand<TEntity> command)
        {
            foreach (var entity in command.Entities)
            {
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
                entity.ModifiedOn = DateTime.UtcNow;

                dbContext.GetCollection<TEntity>().ReplaceOne(filter, entity);
                cache.Update(entity.Key, entity);
            }
        }
    }
}