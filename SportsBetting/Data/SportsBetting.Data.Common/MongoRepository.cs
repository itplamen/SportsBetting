namespace SportsBetting.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using MongoDB.Driver;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;

    public class MongoRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseModel
    {
        private readonly IMongoCollection<TEntity> collection;

        public MongoRepository(ISportsBettingDbContext dbContext)
        {
            this.collection = dbContext.GetCollection<TEntity>();
        }

        public IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filterExpression)
        {
            return collection.Find(filterExpression).ToList();
        }

        public void Add(TEntity entity)
        {
            entity.CreatedOn = DateTime.UtcNow;

            collection.InsertOne(entity);
        }

        public void Update(TEntity entity)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            entity.ModifiedOn = DateTime.UtcNow;

            collection.ReplaceOne(filter, entity);
        }

        public void Delete(TEntity entity)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            UpdateDefinition<TEntity> update = Builders<TEntity>.Update
                .Set(x => x.IsDeleted, true)
                .Set(x => x.DeletedOn, DateTime.Now);

            collection.UpdateOne(filter, update);
        }

        public void HardDelete(TEntity entity)
        {
            collection.DeleteOne(x => x.Id == entity.Id);
        }
    }
}