namespace SportsBetting.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Server.Api.Models.Markets;
    using SportsBetting.Server.Api.Models.Matches;
    using SportsBetting.Server.Api.Models.Odds;

    [EnableCors("*", "*", "*")]
    public class MatchesController : ApiController
    {
        private readonly IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamByIdHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Match>, IEnumerable<Match>> matchByIdHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Category>, IEnumerable<Category>> categoryByIdHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentByIdHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Odd>, IEnumerable<Odd>> oddsByMarketIdHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Market>, IEnumerable<Market>> marketsByMatchIdHandler;

        public MatchesController(
            IQueryHandler<EntitiesByIdQuery<Team>, IEnumerable<Team>> teamByIdHandler,
            IQueryHandler<EntitiesByIdQuery<Match>, IEnumerable<Match>> matchByIdHandler,
            IQueryHandler<EntitiesByIdQuery<Category>, IEnumerable<Category>> categoryByIdHandler,
            IQueryHandler<EntitiesByIdQuery<Tournament>, IEnumerable<Tournament>> tournamentByIdHandler,
            IQueryHandler<EntitiesByIdQuery<Odd>, IEnumerable<Odd>> oddsByMarketIdHandler,
            IQueryHandler<EntitiesByIdQuery<Market>, IEnumerable<Market>> marketsByMatchIdHandler)
        {
            this.teamByIdHandler = teamByIdHandler;
            this.matchByIdHandler = matchByIdHandler;
            this.categoryByIdHandler = categoryByIdHandler;
            this.tournamentByIdHandler = tournamentByIdHandler;
            this.oddsByMarketIdHandler = oddsByMarketIdHandler;
            this.marketsByMatchIdHandler = marketsByMatchIdHandler;
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            EntitiesByIdQuery<Match> matchByIdQuery = new EntitiesByIdQuery<Match>(new List<string>() { id });
            Match match = matchByIdHandler.Handle(matchByIdQuery).FirstOrDefault();

            if (match == null)
            {
                return NotFound();
            }

            EntitiesByIdQuery<Category> categoryByIdQuery = new EntitiesByIdQuery<Category>(new List<string>() { match.CategoryId });
            Category category = categoryByIdHandler.Handle(categoryByIdQuery).FirstOrDefault();

            EntitiesByIdQuery<Tournament> tournamentByIdQuery = new EntitiesByIdQuery<Tournament>(new List<string>() { match.TournamentId });
            Tournament tournament = tournamentByIdHandler.Handle(tournamentByIdQuery).FirstOrDefault();

            EntitiesByIdQuery<Team> homeTeamByIdQuery = new EntitiesByIdQuery<Team>(new List<string>() { match.HomeTeamId });
            Team homeTeam = teamByIdHandler.Handle(homeTeamByIdQuery).FirstOrDefault();

            EntitiesByIdQuery<Team> awayTeamByIdQuery = new EntitiesByIdQuery<Team>(new List<string>() { match.AwayTeamId });
            Team awayTeam = teamByIdHandler.Handle(awayTeamByIdQuery).FirstOrDefault();

            IEnumerable<string> matchIds = new List<string>() { match.Id };
            EntitiesByIdQuery<Market> marketsByMatchIdQuery = new EntitiesByIdQuery<Market>(matchIds, x => matchIds.Contains(x.MatchId));
            IEnumerable<Market> markets = marketsByMatchIdHandler.Handle(marketsByMatchIdQuery);

            ICollection<MarketResponseModel> marketsResponseModel = new List<MarketResponseModel>();

            foreach (var market in markets)
            {
                IEnumerable<string> marketIds = new List<string>() { market.Id };
                EntitiesByIdQuery<Odd> oddsByMarketIdQuery = new EntitiesByIdQuery<Odd>(marketIds, x => marketIds.Contains(x.MarketId));
                IEnumerable<Odd> odds = oddsByMarketIdHandler.Handle(oddsByMarketIdQuery);

                MarketResponseModel marketResponseModel = Mapper.Map<MarketResponseModel>(market);
                marketResponseModel.Odds = Mapper.Map<IEnumerable<OddResponseModel>>(odds);

                marketsResponseModel.Add(marketResponseModel);
            }

            MatchResponseModel matchResponseModel = Mapper.Map<MatchResponseModel>(match);
            matchResponseModel.Category = category.Name;
            matchResponseModel.Tournament = tournament.Name;
            matchResponseModel.HomeTeam = homeTeam.Name;
            matchResponseModel.AwayTeam = awayTeam.Name;
            matchResponseModel.Markets = marketsResponseModel;

            return Ok(matchResponseModel);
        }
    }
}