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
    public class EsportsController : ApiController
    {
        private readonly IQueryHandler<EsportsMatchesQuery, IEnumerable<EsportsMatchesResult>> esportsHandler;

        public EsportsController(IQueryHandler<EsportsMatchesQuery, IEnumerable<EsportsMatchesResult>> esportsHandler)
        {
            this.esportsHandler = esportsHandler;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            EsportsMatchesQuery query = new EsportsMatchesQuery() { Take = 20 };
            IEnumerable<EsportsMatchesResult> esports = esportsHandler.Handle(query);
            IEnumerable<GameResponseModel> response = Mapper.Map<IEnumerable<GameResponseModel>>(esports);

            return Ok(response);
        }
    }
}