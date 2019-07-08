namespace SportsBetting.Handlers.Queries.Categories
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CategoriesByIdsQueryHandler : IQueryHandler<CategoriesByIdsQuery, IEnumerable<Category>>
    {
        private readonly ICache<Category> categoriesCache;

        public CategoriesByIdsQueryHandler(ICache<Category> categoriesCache)
        {
            this.categoriesCache = categoriesCache;
        }

        public IEnumerable<Category> Handle(CategoriesByIdsQuery query)
        {
            IEnumerable<Category> categories = categoriesCache.All(x => query.CategoryIds.Contains(x.Id));

            return categories;
        }
    }
}