namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Common.Constants;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Handlers.Commands.Categories;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Categories;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CategoriesManager : ICategoriesManager
    {
        private readonly IQueryHandler<EntitiesByKeyQuery<Sport>, IEnumerable<Sport>> sportsByKeyHandler;
        private readonly IQueryHandler<CategoryByNameQuery, Category> categoryByNameHandler;
        private readonly ICommandHandler<CreateCategoryCommand, string> createCategoryHandler;

        public CategoriesManager(
            IQueryHandler<EntitiesByKeyQuery<Sport>, IEnumerable<Sport>> sportByKeyHandler,
            IQueryHandler<CategoryByNameQuery, Category> categoryByNameHandler,
            ICommandHandler<CreateCategoryCommand, string> createCategoryHandler)
        {
            this.sportsByKeyHandler = sportByKeyHandler;
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

            IEnumerable<int> keys = new List<int>() { CommonConstants.ESPORT_KEY };
            EntitiesByKeyQuery<Sport> sportQuery = new EntitiesByKeyQuery<Sport>(keys);
            Sport sport = sportsByKeyHandler.Handle(sportQuery).First();

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