namespace SportsBetting.Data.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using SportsBetting.Data.Models.Base;

    public interface ICache<TEntity>
        where TEntity : BaseModel
    {
        IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filterExpression);

        void Add(int key, TEntity entity);

        void Update(int key, TEntity entity);

        void Delete(int key, TEntity entity);

        void HardDelete(int key);
    }
}