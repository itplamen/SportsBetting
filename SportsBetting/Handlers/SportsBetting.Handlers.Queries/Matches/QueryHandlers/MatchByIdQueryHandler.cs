namespace SportsBetting.Handlers.Queries.Matches.QueryHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Common.Queries;
    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Markets.Queries;
    using SportsBetting.Handlers.Queries.Matches.Queries;
    using SportsBetting.Handlers.Queries.Odds.Queries;

    public class MatchByIdQueryHandler : IQueryHandler<MatchByIdQuery, MatchResult>
    {
        private readonly IQueryHandler<OddsByMarketIdQuery, IEnumerable<Odd>> oddsHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Match>, IEnumerable<Match>> matchesHandler;
        private readonly IQueryHandler<MarketsByMatchIdQuery, IEnumerable<Market>> marketsHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsHandler;

        public MatchByIdQueryHandler(
            IQueryHandler<OddsByMarketIdQuery, IEnumerable<Odd>> oddsHandler,
            IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsHandler,
            IQueryHandler<EntitiesByIdQuery<Match>, IEnumerable<Match>> matchesHandler,
            IQueryHandler<MarketsByMatchIdQuery, IEnumerable<Market>> marketsHandler,
            IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsHandler)
        {
            this.oddsHandler = oddsHandler;
            this.teamsHandler = teamsHandler;
            this.matchesHandler = matchesHandler;
            this.marketsHandler = marketsHandler;
            this.tournamentsHandler = tournamentsHandler;
        }

        public MatchResult Handle(MatchByIdQuery query)
        {
            Match match = GetMatch(query.Id);

            if (match == null)
            {
                return null;
            }

            ICollection<MarketResult> marketResults = new List<MarketResult>();
            IEnumerable<Market> markets = marketsHandler.Handle(new MarketsByMatchIdQuery(match.Id));

            foreach (var market in markets)
            {
                IEnumerable<Odd> odds = oddsHandler.Handle(new OddsByMarketIdQuery(market.Id));

                if (odds.Any())
                {
                    MarketResult marketResult = Mapper.Map<MarketResult>(market);
                    marketResult.Odds = Mapper.Map<IEnumerable<OddResult>>(odds);

                    marketResults.Add(marketResult);
                }
            }

            MatchResult matchResult = Mapper.Map<MatchResult>(match);
            matchResult.Tournament = GetTournament(match.TournamentId).Name;
            matchResult.HomeTeam = GetTeam(match.HomeTeamId).Name;
            matchResult.AwayTeam = GetTeam(match.AwayTeamId).Name;
            matchResult.Markets = marketResults;

            return matchResult;
        }

        private Match GetMatch(string id)
        {
            IEnumerable<string> ids = new List<string>() { id };
            EntitiesByIdQuery<Match> matchByIdQuery = new EntitiesByIdQuery<Match>(ids);
            Match match = matchesHandler.Handle(matchByIdQuery).FirstOrDefault();

            return match;
        }

        private Tournament GetTournament(string id)
        {
            IEnumerable<string> ids = new List<string>() { id };
            EntitiesByIdQuery<Tournament> tournamentByIdQuery = new EntitiesByIdQuery<Tournament>(ids);
            Tournament tournament = tournamentsHandler.Handle(tournamentByIdQuery).FirstOrDefault();

            return tournament;
        }

        private Team GetTeam(string id)
        {
            IEnumerable<string> ids = new List<string>() { id };
            EntitiesByIdQuery<Team> teamByIdQuery = new EntitiesByIdQuery<Team>(ids);
            Team team = teamsHandler.Handle(teamByIdQuery).FirstOrDefault();

            return team;
        }
    }
}