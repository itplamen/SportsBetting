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

        public string Add(string name, string sportId)
        {
            Category category = new Category()
            {
                Key = Math.Abs(name.GetHashCode()),
                Name = name,
                SportId = sportId
            };

            categoriesRepository.Add(category);

            return category.Id;
        }

        public Category Get(string name)
        {
            Category category = categoriesRepository.All(x => x.Name == name).FirstOrDefault();

            return category;
        }

        public IEnumerable<Category> All()
        {
            IEnumerable<Category> categories = categoriesRepository.All(x => !x.IsDeleted);

            return categories;
        }
    }
}