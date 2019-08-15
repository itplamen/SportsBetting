namespace SportsBetting.Data.Cache.General
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models.Base;

    public abstract class BaseCache<TEntity> : ICache<TEntity>, ICacheLoader
        where TEntity : BaseModel
    {
        private readonly int refreshInterval;
        private readonly IDictionary<int, TEntity> cache;

        public BaseCache()
            : this(1000 * 60)
        {
        }

        public BaseCache(int refreshInterval)
        {
            this.refreshInterval = refreshInterval;
            this.cache = new ConcurrentDictionary<int, TEntity>();
        }

        public IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filterExpression)
        {
            return cache.Select(x => x.Value)
                   .AsQueryable()
                   .Where(filterExpression);
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

        public abstract void Init();
        
        public abstract void Refresh();
    }
}