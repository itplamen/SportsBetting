namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Handlers.Commands.Categories;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Services.Data.Contracts;

    public class CategoriesManager : ICategoriesManager
    {
        private readonly IRepository<Sport> sportsRepository;
        private readonly ICategoriesService categoriesService;
        private readonly ICommandHandler<CreateCategoryCommand, string> createCategoryHandler;

        public CategoriesManager(
            IRepository<Sport> sportsRepository, 
            ICategoriesService categoriesService,
            ICommandHandler<CreateCategoryCommand, string> createCategoryHandler)
        {
            this.sportsRepository = sportsRepository;
            this.categoriesService = categoriesService;
            this.createCategoryHandler = createCategoryHandler;
        }

        public string Manage(string name)
        {
            Category category = categoriesService.Get(name);

            if (category != null)
            {
                return category.Id;
            }

            Sport sport = sportsRepository.All(x => x.Key == 1).FirstOrDefault();

            CreateCategoryCommand command = new CreateCategoryCommand()
            {
                Key = name.GetHashCode(),
                Name = name,
                SportId = sport.Id
            };

            return createCategoryHandler.Handle(command);
        }
    }
}