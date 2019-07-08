namespace SportsBetting.Handlers.Queries.Common
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntitiesByIdQuery<TEntity> : IQuery<IEnumerable<TEntity>>
        where TEntity : BaseModel
    {
        public EntitiesByIdQuery(IEnumerable<string> ids)
        {
            Ids = ids;
        }

        public IEnumerable<string> Ids { get; set; }
    }
}