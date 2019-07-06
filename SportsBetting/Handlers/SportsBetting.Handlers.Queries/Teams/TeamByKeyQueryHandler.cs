namespace SportsBetting.Handlers.Queries.Teams
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TeamByKeyQueryHandler : IQueryHandler<TeamByKeyQuery, Team>
    {
        private readonly ICache<Team> teamsCache;

        public TeamByKeyQueryHandler(ICache<Team> teamsCache)
        {
            this.teamsCache = teamsCache;
        }

        public Team Handle(TeamByKeyQuery query)
        {
            Team team = teamsCache.All(x => x.Key == query.Key).FirstOrDefault();

            return team;
        }
    }
}