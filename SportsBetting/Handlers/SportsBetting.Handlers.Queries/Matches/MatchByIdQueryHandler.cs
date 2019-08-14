namespace SportsBetting.Handlers.Queries.Matches
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MatchByIdQueryHandler : IQueryHandler<MatchByIdQuery, MatchResult>
    {
        private readonly IQueryHandler<EntitiesByIdQuery<Odd>, IEnumerable<Odd>> oddsHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Match>, IEnumerable<Match>> matchesHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Market>, IEnumerable<Market>> marketsHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Category>, IEnumerable<Category>> categoriesHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsHandler;

        public MatchByIdQueryHandler(
            IQueryHandler<EntitiesByIdQuery<Odd>, IEnumerable<Odd>> oddsHandler,
            IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamsHandler,
            IQueryHandler<EntitiesByIdQuery<Match>, IEnumerable<Match>> matchesHandler,
            IQueryHandler<EntitiesByIdQuery<Market>, IEnumerable<Market>> marketsHandler,
            IQueryHandler<EntitiesByIdQuery<Category>, IEnumerable<Category>> categoriesHandler,
            IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentsHandler)
        {
            this.oddsHandler = oddsHandler;
            this.teamsHandler = teamsHandler;
            this.matchesHandler = matchesHandler;
            this.marketsHandler = marketsHandler;
            this.categoriesHandler = categoriesHandler;
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
            IEnumerable<Market> markets = GetMarkets(match.Id);

            foreach (var market in markets)
            {
                IEnumerable<Odd> odds = GetOdds(market.Id);

                if (odds.Any())
                {
                    MarketResult marketResult = Mapper.Map<MarketResult>(market);
                    marketResult.Odds = Mapper.Map<IEnumerable<OddResult>>(odds);

                    marketResults.Add(marketResult);
                }
            }

            MatchResult matchResult = Mapper.Map<MatchResult>(match);
            matchResult.Category = GetCategory(match.CategoryId).Name;
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
        
        private Category GetCategory(string id)
        {
            IEnumerable<string> ids = new List<string>() { id };
            EntitiesByIdQuery<Category> categoryByIdQuery = new EntitiesByIdQuery<Category>(ids);
            Category category = categoriesHandler.Handle(categoryByIdQuery).FirstOrDefault();

            return category;
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

        private IEnumerable<Market> GetMarkets(string matchId)
        {
            IEnumerable<string> matchIds = new List<string>() { matchId };
            EntitiesByIdQuery<Market> marketsByMatchIdQuery = new EntitiesByIdQuery<Market>(matchIds, x => matchIds.Contains(x.MatchId));
            IEnumerable<Market> markets = marketsHandler.Handle(marketsByMatchIdQuery);

            return markets;
        }

        private IEnumerable<Odd> GetOdds(string marketId)
        {
            IEnumerable<string> marketIds = new List<string>() { marketId };
            EntitiesByIdQuery<Odd> oddsByMarketIdQuery = new EntitiesByIdQuery<Odd>(marketIds, x => marketIds.Contains(x.MarketId));
            IEnumerable<Odd> odds = oddsHandler.Handle(oddsByMarketIdQuery);

            return odds;
        }
    }
}