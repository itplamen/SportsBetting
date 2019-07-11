namespace SportsBetting.Clients.Web.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Queries.Matches;

    public class SelectGamesViewModel : IMapTo<UpcomingMatchesQuery>
    {
        public SelectGamesViewModel()
        {
            Take = 20;
        }

        [Required]
        [Range(1, 100)]
        public int Take { get; set; }

        [Required]
        public string Token { get; set; }
    }
}