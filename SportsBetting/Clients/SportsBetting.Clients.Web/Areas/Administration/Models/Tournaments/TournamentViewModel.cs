namespace SportsBetting.Clients.Web.Areas.Administration.Models.Tournaments
{
    using SportsBetting.Clients.Web.Areas.Administration.Models.Base;
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class TournamentViewModel : BaseViewModel, IMapFrom<Tournament>
    {
        public string Name { get; set; }

        public string CategoryId { get; set; }
    }
}