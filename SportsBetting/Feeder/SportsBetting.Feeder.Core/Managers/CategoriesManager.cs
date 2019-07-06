namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Handlers.Commands.Categories;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Categories;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CategoriesManager : ICategoriesManager
    {
        private readonly IRepository<Sport> sportsRepository;
        private readonly IQueryHandler<CategoryByNameQuery, Category> categoryByNameHandler;
        private readonly ICommandHandler<CreateCategoryCommand, string> createCategoryHandler;

        public CategoriesManager(
            IRepository<Sport> sportsRepository,
            IQueryHandler<CategoryByNameQuery, Category> categoryByNameHandler,
            ICommandHandler<CreateCategoryCommand, string> createCategoryHandler)
        {
            this.sportsRepository = sportsRepository;
            this.categoryByNameHandler = categoryByNameHandler;
            this.createCategoryHandler = createCategoryHandler;
        }

        public string Manage(string name)
        {
            CategoryByNameQuery query = new CategoryByNameQuery(name);
            Category category = categoryByNameHandler.Handle(query);

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