namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class CategoriesCache : BaseCache<Category>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

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
                Add(category.Key, category);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);
            IEnumerable<Category> categories = categoriesRepository.Load(x => 
                !x.IsDeleted && 
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value >= dateTime);

            foreach (var category in categories)
            {
                Update(category.Key, category);
            }
        }
    }
}