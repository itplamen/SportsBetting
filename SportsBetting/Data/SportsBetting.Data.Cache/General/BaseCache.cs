namespace SportsBetting.Data.Cache.General
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models.Base;

    public abstract class BaseCache<TEntity> : ICache<TEntity>, ICacheInitializer
        where TEntity : BaseModel
    {
        private readonly int load;
        private readonly int update;

        public BaseCache()
            : this(1000, 1000 * 60)
        {
        }

        public BaseCache(int load, int update)
        {
            this.load = load;
            this.update = update;
            this.Cache = new ConcurrentDictionary<int, TEntity>();
        }

        protected IDictionary<int, TEntity> Cache { get; private set; }

        public void Init()
        {
            Timer timer = new Timer((_) => Load(), null, load, update);
        }

        public IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Cache.Select(x => x.Value)
                   .AsQueryable()
                   .Where(filterExpression);
        }

        public void Add(int key, TEntity entity)
        {
            Cache[key] = entity;
        }

        public void Delete(int key, TEntity entity)
        {
            TEntity cachedEntity = Cache[key];

            if (cachedEntity != null)
            {
                cachedEntity.IsDeleted = entity.IsDeleted;
                cachedEntity.DeletedOn = entity.DeletedOn;

                Cache[key] = cachedEntity;
            }
        }

        public void HardDelete(int key)
        {
            Cache.Remove(key);
        }

        public abstract void Load();
    }
}