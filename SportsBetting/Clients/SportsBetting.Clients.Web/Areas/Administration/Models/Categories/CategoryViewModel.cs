namespace SportsBetting.Clients.Web.Areas.Administration.Models.Categories
{
    using System;

    using SportsBetting.Clients.Web.Mapping;
    using SportsBetting.Data.Models;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public int Key { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Name { get; set; }

        public string SportId { get; set; }
    }
}