namespace SportsBetting.Data.Common.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models.Base;

    public interface IRepository<T>
         where T : BaseModel
    {
        IEnumerable<T> All();

        IEnumerable<T> AllWithDeleted();

        T GetById(string id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);
    }
}