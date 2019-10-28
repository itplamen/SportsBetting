namespace SportsBetting.Handlers.Queries.Teams.QueryHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Common.Queries;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Teams.Queries;

    public class TeamsByIdsQueryHandler : IQueryHandler<TeamsByIdsQuery, IEnumerable<Team>>
    {
        private readonly IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsHandler;

        public TeamsByIdsQueryHandler(IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsHandler)
        {
            this.teamsHandler = teamsHandler;
        }

        public IEnumerable<Team> Handle(TeamsByIdsQuery query)
        {
            List<string> teamIds = new List<string>();
            teamIds.AddRange(query.HomeTeamIds);
            teamIds.AddRange(query.AwayTeamIds);
            teamIds.Distinct();

            EntitiesByIdQuery<Team> teamsQuery = new EntitiesByIdQuery<Team>(teamIds);
            IEnumerable<Team> teams = teamsHandler.Handle(teamsQuery);

            return teams;
        }
    }
}