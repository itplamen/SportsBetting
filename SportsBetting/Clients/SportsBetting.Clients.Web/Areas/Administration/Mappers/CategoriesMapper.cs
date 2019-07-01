namespace SportsBetting.Clients.Web.Areas.Administration.Mappers
{
    using System.Collections.Generic;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Categories;
    using SportsBetting.Common.Contracts;
    using SportsBetting.Data.Models;

    public class CategoriesMapper : IMapper<Category, CategoryViewModel>
    {
        public CategoryViewModel Map(Category from)
        {
            CategoryViewModel mapped = new CategoryViewModel()
            {
                Id = from.Id,
                Key = from.Key,
                CreatedOn = from.CreatedOn,
                ModifiedOn = from.ModifiedOn,
                IsDeleted = from.IsDeleted,
                DeletedOn = from.DeletedOn,
                Name = from.Name
            };

            return mapped;
        }

        public IEnumerable<CategoryViewModel> Map(IEnumerable<Category> from)
        {
            ICollection<CategoryViewModel> mapped = new List<CategoryViewModel>();

            foreach (var category in from)
            {
                mapped.Add(Map(category));
            }

            return mapped;
        }
    }
}