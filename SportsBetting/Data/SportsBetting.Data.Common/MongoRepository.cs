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

    public class MongoRepository<T> : IRepository<T>
        where T : BaseModel
    {
        private readonly IMongoCollection<T> collection;

        public MongoRepository(ISportsBettingDbContext dbContext)
        {
            this.collection = dbContext.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> All(Expression<Func<T, bool>> filterExpression)
        {
            return collection.Find(filterExpression).ToList();
        }

        public void Add(T entity)
        {
            entity.CreatedOn = DateTime.UtcNow;

            collection.InsertOne(entity);
        }

        public void Delete(T entity)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            UpdateDefinition<T> update = Builders<T>.Update.Set(x => x.IsDeleted, true);

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            collection.UpdateOne(filter, update);
        }

        public void HardDelete(T entity)
        {
            collection.DeleteOne(x => x.Id == entity.Id);
        }
    }
}