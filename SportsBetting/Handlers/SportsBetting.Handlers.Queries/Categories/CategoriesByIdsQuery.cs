namespace SportsBetting.Handlers.Queries.Categories
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CategoriesByIdsQuery : IQuery<IEnumerable<Category>>
    {
        public CategoriesByIdsQuery(IEnumerable<string> categoryIds)
        {
            CategoryIds = categoryIds;
        }

        public IEnumerable<string> CategoryIds { get; set; }
    }
}