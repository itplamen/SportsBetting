namespace SportsBetting.Handlers.Commands.Categories
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateCategoryCommand : ICommand, IMapTo<Category>
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public string SportId { get; set; }
    }
}