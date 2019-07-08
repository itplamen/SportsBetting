namespace SportsBetting.Handlers.Queries.Categories
{
    using System.Linq;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CategoryByNameQueryHandler : IQueryHandler<CategoryByNameQuery, Category>
    {
        private readonly ICache<Category> categoriesCache;

        public CategoryByNameQueryHandler(ICache<Category> categoriesCache)
        {
            this.categoriesCache = categoriesCache;
        }

        public Category Handle(CategoryByNameQuery query)
        {
            Category category = categoriesCache.All(x => x.Name == query.Name).FirstOrDefault();

            return category;
        }
    }
}