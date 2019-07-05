namespace SportsBetting.Data.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using SportsBetting.Data.Models.Base;

    public interface IRepository<TEntity>
         where TEntity : BaseModel
    {
        IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filterExpression);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void HardDelete(TEntity entity);
    }
}