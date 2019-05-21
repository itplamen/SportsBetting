namespace SportsBetting.Data.Cache
{
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class CategoriesCache : BaseCache<int, Category>
    {
        private readonly ICacheLoaderRepository<Category> categoriesRepository;

        public CategoriesCache(ICacheLoaderRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public override void Load()
        {
            IEnumerable<Category> categories = categoriesRepository.Load(x => !x.IsDeleted);

            foreach (var category in categories)
            {
                Cache[category.Key] = category;
            }
        }
    }
}