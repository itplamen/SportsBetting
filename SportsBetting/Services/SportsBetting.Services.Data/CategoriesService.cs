namespace SportsBetting.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<Category> Get(IEnumerable<string> categoryIds)
        {
            IEnumerable<Category> categories = categoriesRepository.All(x => !x.IsDeleted && categoryIds.Contains(x.Id));

            return categories;
        }

        public IEnumerable<Category> AllWithDeleted()
        {
            IEnumerable<Category> categories = categoriesRepository.All(x => true);

            return categories;
        }
    }
}