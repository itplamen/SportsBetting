namespace SportsBetting.Data.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using SportsBetting.Data.Models.Base;

    public interface ICache<TKey, TEntity>
        where TEntity : BaseModel
    {
        IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filterExpression);

        void Add(TKey key, TEntity entity);

        void Delete(TKey key, TEntity entity);

        void HardDelete(TKey key);
    }
}