namespace SportsBetting.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using MongoDB.Driver;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;

    public class CacheLoaderRepository<TEntity> : ICacheLoaderRepository<TEntity>
        where TEntity : BaseModel
    {
        private readonly IMongoCollection<TEntity> collection;

        public CacheLoaderRepository(ISportsBettingDbContext dbContext)
        {
            this.collection = dbContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public IEnumerable<TEntity> Load(Expression<Func<TEntity, bool>> filterExpression)
        {
            return collection.Find(filterExpression).ToList();
        }
    }
}