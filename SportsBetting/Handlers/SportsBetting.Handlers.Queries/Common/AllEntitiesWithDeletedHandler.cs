namespace SportsBetting.Handlers.Queries.Common
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AllEntitiesWithDeletedHandler<TEntity> : IQueryHandler<IEnumerable<TEntity>>
        where TEntity : BaseModel
    {
        private readonly ISportsBettingDbContext dbContext;

        public AllEntitiesWithDeletedHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TEntity> Handle()
        {
            IEnumerable<TEntity> entities = dbContext.GetCollection<TEntity>().Find(_ => true).ToList();

            return entities;
        }
    }
}