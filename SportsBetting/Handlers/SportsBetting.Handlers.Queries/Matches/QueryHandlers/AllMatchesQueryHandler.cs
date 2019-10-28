namespace SportsBetting.Handlers.Queries.Matches.QueryHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Common.Queries;
    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches.Queries;
    using SportsBetting.Handlers.Queries.Teams.Queries;

    public class AllMatchesQueryHandler : IQueryHandler<AllMatchesQuery, IEnumerable<MatchResult>>
    {
        private readonly ICache<Match> matchesCache;
        private readonly IQueryHandler<TeamsByIdsQuery, IEnumerable<Team>> teamsHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsHandler;

        public AllMatchesQueryHandler(
            ICache<Match> matchesCache,
            IQueryHandler<TeamsByIdsQuery, IEnumerable<Team>> teamsHandler,
            IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsHandler)
        {
            this.matchesCache = matchesCache;
            this.teamsHandler = teamsHandler;
            this.tournamentsHandler = tournamentsHandler;
        }

        public IEnumerable<MatchResult> Handle(AllMatchesQuery query)
        {
            IEnumerable<Match> matches = matchesCache.All(_ => true).OrderBy(x => x.Type).Take(query.Take);

            EntitiesByIdQuery<Tournament> tournamentsQuery = new EntitiesByIdQuery<Tournament>(matches.Select(x => x.TournamentId).Distinct());
            IEnumerable<Tournament> tournaments = tournamentsHandler.Handle(tournamentsQuery);

            TeamsByIdsQuery teamsQuery = new TeamsByIdsQuery(matches.Select(x => x.HomeTeamId), matches.Select(x => x.AwayTeamId));
            IEnumerable<Team> teams = teamsHandler.Handle(teamsQuery);

            ICollection<MatchResult> matchResults = new List<MatchResult>();

            foreach (var match in matches)
            {
                MatchResult matchResult = Mapper.Map<MatchResult>(match);
                matchResult.HomeTeam = teams.First(x => x.Id == match.HomeTeamId).Name;
                matchResult.AwayTeam = teams.First(x => x.Id == match.AwayTeamId).Name;
                matchResult.Tournament = tournaments.First(x => x.Id == match.TournamentId).Name;

                matchResults.Add(matchResult);
            }

            return matchResults;
        }
    }
}