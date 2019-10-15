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
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public CategoriesManager(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        public string Manage(string name)
        {
            CategoryByNameQuery categoryQuery = new CategoryByNameQuery(name);
            Category category = queryDispatcher.Dispatch<CategoryByNameQuery, Category>(categoryQuery);

            if (category != null)
            {
                return category.Id;
            }

            IEnumerable<int> keys = new List<int>() { CommonConstants.ESPORT_KEY };
            EntitiesByKeyQuery<Sport> sportQuery = new EntitiesByKeyQuery<Sport>(keys);
            Sport sport = queryDispatcher.Dispatch<EntitiesByKeyQuery<Sport>, IEnumerable<Sport>>(sportQuery).First();

            CreateCategoryCommand categoryCommand = new CreateCategoryCommand()
            {
                Key = name.GetHashCode(),
                Name = name,
                SportId = sport.Id
            };

            return commandDispatcher.Dispatch<CreateCategoryCommand, string>(categoryCommand);
        }
    }
}