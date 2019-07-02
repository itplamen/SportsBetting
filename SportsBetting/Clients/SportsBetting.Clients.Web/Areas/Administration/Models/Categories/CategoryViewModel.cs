namespace SportsBetting.Clients.Web.Areas.Administration.Models.Categories
{
    using SportsBetting.Clients.Web.Areas.Administration.Models.Base;
    using SportsBetting.Clients.Web.Mapping;
    using SportsBetting.Data.Models;

    public class CategoryViewModel : BaseViewModel, IMapFrom<Category>
    {
        public string Name { get; set; }

        public string SportId { get; set; }
    }
}