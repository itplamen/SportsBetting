namespace SportsBetting.Handlers.Queries.Categories
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AllCategoriesWithDeletedHandler : IQueryHandler<IEnumerable<Category>>
    {
        private readonly ISportsBettingDbContext dbContext;

        public AllCategoriesWithDeletedHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Category> Handle()
        {
            IEnumerable<Category> categories = dbContext.GetCollection<Category>().Find(x => true).ToList();

            return categories;
        }
    }
}