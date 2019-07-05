namespace SportsBetting.Data.Cache.General
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;

    using MongoDB.Driver;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;

    public abstract class BaseCache<TEntity> : ICache<TEntity>, ICacheInitializer
        where TEntity : BaseModel
    {
        private readonly int refreshInterval;
        private readonly IDictionary<int, TEntity> cache;
        private readonly ISportsBettingDbContext dbContext;

        public BaseCache(ISportsBettingDbContext dbContext)
            : this(dbContext, 1000 * 60)
        {
        }

        public BaseCache(ISportsBettingDbContext dbContext, int refreshInterval)
        {
            this.dbContext = dbContext;
            this.refreshInterval = refreshInterval;
            this.cache = new ConcurrentDictionary<int, TEntity>();
        }

        public void Init()
        {
            Timer timer = new Timer((_) => Refresh(), null, refreshInterval, refreshInterval);
            Load();
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

        public void Delete(int key, TEntity entity)
        {
            TEntity cachedEntity = cache[key];

            if (cachedEntity != null)
            {
                cachedEntity.IsDeleted = entity.IsDeleted;
                cachedEntity.DeletedOn = entity.DeletedOn;

                cache[key] = cachedEntity;
            }
        }

        public void HardDelete(int key)
        {
            cache.Remove(key);
        }

        public abstract void Load();

        public abstract void Refresh();

        protected IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> expression)
        {
            return dbContext.GetCollection<TEntity>(typeof(TEntity).Name)
                .Find(expression)
                .ToList();
        }
    }
}