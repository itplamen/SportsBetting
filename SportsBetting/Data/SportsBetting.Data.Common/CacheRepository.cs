namespace SportsBetting.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models.Base;

    public class CacheRepository<T> : IRepository<T>
        where T : BaseModel
    {
        private readonly ICache<int, T> cache;
        private readonly IRepository<T> repository;

        public CacheRepository(ICache<int, T> cache, IRepository<T> repository)
        {
            this.cache = cache;
            this.repository = repository;
        }

        public IEnumerable<T> All(Expression<Func<T, bool>> filterExpression)
        {
            return cache.All(filterExpression);
        }

        public void Add(T entity)
        {
            repository.Add(entity);
            cache.Add(entity.Key, entity);
        }

        public void Delete(T entity)
        {
            repository.Delete(entity);
            cache.Delete(entity.Key, entity);
        }

        public void HardDelete(T entity)
        {
            repository.HardDelete(entity);
            cache.HardDelete(entity.Key);
        }
    }
}