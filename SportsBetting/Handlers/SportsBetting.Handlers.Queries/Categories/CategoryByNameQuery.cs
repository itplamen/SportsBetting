namespace SportsBetting.Handlers.Queries.Categories
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CategoryByNameQuery : IQuery<Category>
    {
        public CategoryByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}