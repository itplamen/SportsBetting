namespace SportsBetting.Data.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using SportsBetting.Data.Models.Base;

    public interface IRepository<T>
         where T : BaseModel
    {
        IEnumerable<T> All(Expression<Func<T, bool>> filterExpression);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);
    }
}