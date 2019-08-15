namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;

    public class Cache<TEntity> : ICache<TEntity>, ICacheLoader
        where TEntity : BaseModel
    {
        private const int REFRESH_INTERVAL = 1000 * 3;

        private readonly IDictionary<int, TEntity> cache;
        private readonly ISportsBettingDbContext dbContext;

        public Cache(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.cache = new ConcurrentDictionary<int, TEntity>();
        }

        public void Init()
        {
            IEnumerable<TEntity> entities = dbContext.GetCollection<TEntity>()
                .Find(x => !x.IsDeleted)
                .ToList();

            foreach (var entity in entities)
            {
                Add(entity.Key, entity);
            }
        }

        public void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<TEntity> entities = dbContext.GetCollection<TEntity>()
                .Find(x =>
                    !x.IsDeleted &&
                    (x.CreatedOn >= dateTime ||
                    (x.ModifiedOn.HasValue &&
                    x.ModifiedOn.Value >= dateTime)))
                .ToList();

            foreach (var entity in entities)
            {
                Update(entity.Key, entity);
            }
        }

        public IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filterExpression)
        {
            IEnumerable<TEntity> entities = cache.Select(x => x.Value).AsQueryable().Where(filterExpression);

            return entities;
        }

        public void Add(int key, TEntity entity)
        {
            cache.Add(key, entity);
        }

        public void Update(int key, TEntity entity)
        {
            cache[key] = entity;
        }

        public bool Delete(int key)
        {
            return cache.Remove(key);
        }
    }
}