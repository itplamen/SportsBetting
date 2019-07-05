namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class CategoriesCache : BaseCache<Category>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        private readonly ISportsBettingDbContext dbContext;

        public CategoriesCache(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Load()
        {
            IEnumerable<Category> categories = dbContext.GetCollection<Category>()
                .Find(x => !x.IsDeleted)
                .ToList();

            foreach (var category in categories)
            {
                Add(category.Key, category);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Category> categories = dbContext.GetCollection<Category>()
                .Find(x => 
                    !x.IsDeleted && 
                    x.ModifiedOn.HasValue &&
                    x.ModifiedOn.Value >= dateTime)
                .ToList();

            foreach (var category in categories)
            {
                Update(category.Key, category);
            }
        }
    }
}