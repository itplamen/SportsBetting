namespace SportsBetting.Handlers.Queries.Matches
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AllMatchesQueryHandler : IQueryHandler<AllMatchesQuery, IEnumerable<MatchResult>>
    {
        private readonly ICache<Match> matchesCache;
        private readonly IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsHandler;

        public AllMatchesQueryHandler(
            ICache<Match> matchesCache,
            IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsHandler,
            IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsHandler)
        {
            this.matchesCache = matchesCache;
            this.teamsHandler = teamsHandler;
            this.tournamentsHandler = tournamentsHandler;
        }

        public IEnumerable<MatchResult> Handle(AllMatchesQuery query)
        {
            IEnumerable<Match> matches = matchesCache.All(_ => true).Take(query.Take);
            IEnumerable<Tournament> tournaments = GetTournaments(matches.Select(x => x.TournamentId));
            IEnumerable<Team> teams = GetTeams(matches.Select(x => x.HomeTeamId), matches.Select(x => x.AwayTeamId));

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

        private IEnumerable<Tournament> GetTournaments(IEnumerable<string> tournamentIds)
        {
            EntitiesByIdQuery<Tournament> query = new EntitiesByIdQuery<Tournament>(tournamentIds.Distinct());
            IEnumerable<Tournament> tournaments = tournamentsHandler.Handle(query);

            return tournaments;
        }

        private IEnumerable<Team> GetTeams(IEnumerable<string> homeTeamIds, IEnumerable<string> awayTeamIds)
        {
            List<string> teamIds = new List<string>();
            teamIds.AddRange(homeTeamIds);
            teamIds.AddRange(awayTeamIds);
            teamIds.Distinct();

            EntitiesByIdQuery<Team> query = new EntitiesByIdQuery<Team>(teamIds);
            IEnumerable<Team> teams = teamsHandler.Handle(query);

            return teams;
        }
    }
}