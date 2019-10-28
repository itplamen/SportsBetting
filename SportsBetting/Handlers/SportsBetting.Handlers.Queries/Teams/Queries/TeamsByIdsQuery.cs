namespace SportsBetting.Handlers.Queries.Teams.Queries
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TeamsByIdsQuery : IQuery<IEnumerable<Team>>
    {
        public TeamsByIdsQuery(IEnumerable<string> homeTeamIds, IEnumerable<string> awayTeamIds)
        {
            HomeTeamIds = homeTeamIds;
            AwayTeamIds = awayTeamIds;
        }

        public IEnumerable<string> HomeTeamIds { get; set; }

        public IEnumerable<string> AwayTeamIds { get; set; }
    }
}