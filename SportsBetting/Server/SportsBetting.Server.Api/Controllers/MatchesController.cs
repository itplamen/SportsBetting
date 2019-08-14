namespace SportsBetting.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches;
    using SportsBetting.Server.Api.Models.Matches;

    [EnableCors("*", "*", "*")]
    public class MatchesController : ApiController
    {
        private readonly IQueryHandler<MatchByIdQuery, MatchResult> matchesByIdHandler;
        private readonly IQueryHandler<AllMatchesQuery, IEnumerable<MatchResult>> allMatchesHandler;

        public MatchesController(
            IQueryHandler<MatchByIdQuery, MatchResult> matchesByIdHandler,
            IQueryHandler<AllMatchesQuery, IEnumerable<MatchResult>> allMatchesHandler)
        {
            this.matchesByIdHandler = matchesByIdHandler;
            this.allMatchesHandler = allMatchesHandler;
        }

        [HttpGet]
        public IHttpActionResult All(int take)
        {
            if (take <= 0)
            {
                ModelState.AddModelError("Take", "Invalid request!");

                return BadRequest(ModelState);
            }

            AllMatchesQuery query = new AllMatchesQuery(take);
            IEnumerable<MatchResult> matches = allMatchesHandler.Handle(query);
            IEnumerable<MatchResponseModel> responseModel = Mapper.Map<IEnumerable<MatchResponseModel>>(matches);

            return Ok(responseModel);
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("Id", "Invalid request!");

                return BadRequest(ModelState);
            }

            MatchByIdQuery query = new MatchByIdQuery(id);
            MatchResult result = matchesByIdHandler.Handle(query);

            if (result == null)
            {
                return NotFound();
            }

            MatchResponseModel responseModel = Mapper.Map<MatchResponseModel>(result);

            return Ok(responseModel);
        }
    }
}