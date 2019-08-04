namespace SportsBetting.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches;
    using SportsBetting.Server.Api.Models.UpcomingGames;

    [EnableCors("*", "*", "*")]
    public class UpcomingGamesController : ApiController
    {
        private readonly IQueryHandler<UpcomingMatchesQuery, IEnumerable<UpcomingMatchesResult>> upcomingMatchesHandler;

        public UpcomingGamesController(IQueryHandler<UpcomingMatchesQuery, IEnumerable<UpcomingMatchesResult>> upcomingMatchesHandler)
        {
            this.upcomingMatchesHandler = upcomingMatchesHandler;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            UpcomingMatchesQuery query = new UpcomingMatchesQuery() { Take = 20 };
            IEnumerable<UpcomingMatchesResult> upcomingMatches = upcomingMatchesHandler.Handle(query);
            IEnumerable<GameResponseModel> response = Mapper.Map<IEnumerable<GameResponseModel>>(upcomingMatches);

            return Ok(response);
        }
    }
}