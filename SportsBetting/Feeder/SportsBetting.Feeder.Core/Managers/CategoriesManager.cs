namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Services.Data.Contracts;

    public class CategoriesManager : ICategoriesManager
    {
        private readonly IRepository<Sport> sportsRepository;
        private readonly ICategoriesService categoriesService;

        public CategoriesManager(IRepository<Sport> sportsRepository, ICategoriesService categoriesService)
        {
            this.sportsRepository = sportsRepository;
            this.categoriesService = categoriesService;
        }

        public string Manage(string name)
        {
            Category category = categoriesService.Get(name);

            if (category != null)
            {
                return category.Id;
            }

            Sport sport = sportsRepository.All(x => x.Key == 1).FirstOrDefault();

            return categoriesService.Add(name, sport.Id);
        }
    }
}