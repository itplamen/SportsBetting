namespace SportsBetting.Data.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using SportsBetting.Data.Models.Base;

    public interface ICacheLoaderRepository<TEntity>
        where TEntity : BaseModel
    {
        IEnumerable<TEntity> Load(Expression<Func<TEntity, bool>> filterExpression);
    }
}