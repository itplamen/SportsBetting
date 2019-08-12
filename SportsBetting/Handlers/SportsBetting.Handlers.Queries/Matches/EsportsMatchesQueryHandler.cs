namespace SportsBetting.Handlers.Queries.Matches
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EsportsMatchesQueryHandler : IQueryHandler<EsportsMatchesQuery, IEnumerable<EsportsMatchesResult>>
    {
        private readonly ICache<Match> matchesCache;
        private readonly IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsByIdHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Category>, IEnumerable<Category>> categoriesByIdHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsByIdHandler;

        public EsportsMatchesQueryHandler(
            ICache<Match> matchesCache,
            IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsByIdHandler,
            IQueryHandler<EntitiesByIdQuery<Category>, IEnumerable<Category>> categoriesByIdHandler,
            IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsByIdHandler)
        {
            this.matchesCache = matchesCache;
            this.teamsByIdHandler = teamsByIdHandler;
            this.categoriesByIdHandler = categoriesByIdHandler;
            this.tournamentsByIdHandler = tournamentsByIdHandler;
        }

        public IEnumerable<EsportsMatchesResult> Handle(EsportsMatchesQuery query)
        {
            IEnumerable<Match> matches = matchesCache.All(_ => true).OrderBy(x => x.StartTime).Take(query.Take);
            IEnumerable<Category> categories = GetCategories(matches.Select(x => x.CategoryId));
            IEnumerable<Tournament> tournaments = GetTournaments(matches.Select(x => x.TournamentId));
            IEnumerable<Team> teams = GetTeams(matches.Select(x => x.HomeTeamId), matches.Select(x => x.AwayTeamId));

            ICollection<EsportsMatchesResult> upcomingMatches = new List<EsportsMatchesResult>();

            foreach (var match in matches)
            {
                EsportsMatchesResult upcomingMatch = Mapper.Map<EsportsMatchesResult>(match);
                upcomingMatch.HomeTeam = teams.First(x => x.Id == match.HomeTeamId).Name;
                upcomingMatch.AwayTeam = teams.First(x => x.Id == match.AwayTeamId).Name;
                upcomingMatch.Category = categories.First(x => x.Id == match.CategoryId).Name;
                upcomingMatch.Tournament = tournaments.First(x => x.Id == match.TournamentId).Name;

                upcomingMatches.Add(upcomingMatch);
            }

            return upcomingMatches;
        }

        private IEnumerable<Category> GetCategories(IEnumerable<string> categoryIds)
        {
            EntitiesByIdQuery<Category> query = new EntitiesByIdQuery<Category>(categoryIds.Distinct());
            IEnumerable<Category> categories = categoriesByIdHandler.Handle(query);

            return categories;
        }

        private IEnumerable<Tournament> GetTournaments(IEnumerable<string> tournamentIds)
        {
            EntitiesByIdQuery<Tournament> query = new EntitiesByIdQuery<Tournament>(tournamentIds.Distinct());
            IEnumerable<Tournament> tournaments = tournamentsByIdHandler.Handle(query);

            return tournaments;
        }

        private IEnumerable<Team> GetTeams(IEnumerable<string> homeTeamIds, IEnumerable<string> awayTeamIds)
        {
            List<string> teamIds = new List<string>();
            teamIds.AddRange(homeTeamIds);
            teamIds.AddRange(awayTeamIds);
            teamIds.Distinct();

            EntitiesByIdQuery<Team> query = new EntitiesByIdQuery<Team>(teamIds);
            IEnumerable<Team> teams = teamsByIdHandler.Handle(query);

            return teams;
        }
    }
}