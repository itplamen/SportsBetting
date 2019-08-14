namespace SportsBetting.Handlers.Queries.Common
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntitiesByKeyQuery<TEntity> : IQuery<IEnumerable<TEntity>>
        where TEntity : BaseModel
    {
        public EntitiesByKeyQuery(IEnumerable<int> keys)
        {
            Keys = keys;
        }

        public IEnumerable<int> Keys { get; set; }
    }
}