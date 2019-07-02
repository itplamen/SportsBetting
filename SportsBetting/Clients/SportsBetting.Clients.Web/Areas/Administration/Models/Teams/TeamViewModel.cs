namespace SportsBetting.Clients.Web.Areas.Administration.Models.Teams
{
    using SportsBetting.Clients.Web.Areas.Administration.Models.Base;
    using SportsBetting.Clients.Web.Mapping;
    using SportsBetting.Data.Models;

    public class TeamViewModel : BaseViewModel, IMapFrom<Team>
    {
        public string Name { get; set; }

        public string SportId { get; set; }
    }
}