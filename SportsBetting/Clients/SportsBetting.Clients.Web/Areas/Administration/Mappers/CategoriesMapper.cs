namespace SportsBetting.Clients.Web.Areas.Administration.Mappers
{
    using System.Collections.Generic;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Categories;
    using SportsBetting.Data.Models;

    internal static class CategoriesMapper
    {
        internal static CategoryViewModel Map(Category from)
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

        internal static IEnumerable<CategoryViewModel> Map(IEnumerable<Category> from)
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