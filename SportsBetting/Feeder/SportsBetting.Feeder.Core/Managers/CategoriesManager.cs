namespace SportsBetting.Feeder.Core.Managers
{
    using System;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;

    public class CategoriesManager : ICategoriesManager
    {
        private readonly IRepository<Sport> sportsRepository;
        private readonly IRepository<Category> categoriesRepository;

        public CategoriesManager(IRepository<Sport> sportsRepository, IRepository<Category> categoriesRepository)
        {
            this.sportsRepository = sportsRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public string Manage(string name)
        {
            Category category = categoriesRepository.All(x => x.Name == name).FirstOrDefault();

            if (category == null)
            {
                Sport sport = sportsRepository.All(x => x.Key == 1).FirstOrDefault();

                return Add(name, sport.Id);
            }

            return category.Id;
        }

        private string Add(string name, string sportId)
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
    }
}