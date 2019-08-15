namespace SportsBetting.Handlers.Commands.Common
{
    using System;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Commands.Contracts;

    public class DeleteEntitiesCommandHandler<TEntity> : ICommandHandler<DeleteEntitiesCommand<TEntity>>
        where TEntity : BaseModel
    {
        private readonly ICache<TEntity> cache;
        private readonly ISportsBettingDbContext dbContext;

        public DeleteEntitiesCommandHandler(ICache<TEntity> cache, ISportsBettingDbContext dbContext)
        {
            this.cache = cache;
            this.dbContext = dbContext;
        }

        public void Handle(DeleteEntitiesCommand<TEntity> command)
        {
            foreach (var entity in command.Entities)
            {
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
                UpdateDefinition<TEntity> update = Builders<TEntity>.Update
                    .Set(x => x.IsDeleted, true)
                    .Set(x => x.DeletedOn, DateTime.UtcNow);

                dbContext.GetCollection<TEntity>().UpdateOne(filter, update);
                cache.Delete(entity.Key);
            }
        }
    }
}