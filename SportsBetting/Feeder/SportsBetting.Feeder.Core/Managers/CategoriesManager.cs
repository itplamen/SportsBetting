namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Handlers.Commands.Categories;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Categories;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CategoriesManager : ICategoriesManager
    {
        private readonly IQueryHandler<EntityByKeyQuery<Sport>, Sport> sportByKeyHandler;
        private readonly IQueryHandler<CategoryByNameQuery, Category> categoryByNameHandler;
        private readonly ICommandHandler<CreateCategoryCommand, string> createCategoryHandler;

        public CategoriesManager(
            IQueryHandler<EntityByKeyQuery<Sport>, Sport> sportByKeyHandler,
            IQueryHandler<CategoryByNameQuery, Category> categoryByNameHandler,
            ICommandHandler<CreateCategoryCommand, string> createCategoryHandler)
        {
            this.sportByKeyHandler = sportByKeyHandler;
            this.categoryByNameHandler = categoryByNameHandler;
            this.createCategoryHandler = createCategoryHandler;
        }

        public string Manage(string name)
        {
            CategoryByNameQuery categoryQuery = new CategoryByNameQuery(name);
            Category category = categoryByNameHandler.Handle(categoryQuery);

            if (category != null)
            {
                return category.Id;
            }

            EntityByKeyQuery<Sport> sportQuery = new EntityByKeyQuery<Sport>(1);
            Sport sport = sportByKeyHandler.Handle(sportQuery);

            CreateCategoryCommand categoryCommand = new CreateCategoryCommand()
            {
                Key = name.GetHashCode(),
                Name = name,
                SportId = sport.Id
            };

            return createCategoryHandler.Handle(categoryCommand);
        }
    }
}