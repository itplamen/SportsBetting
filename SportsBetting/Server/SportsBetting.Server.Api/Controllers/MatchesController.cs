namespace SportsBetting.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches.Queries;
    using SportsBetting.Server.Api.Models.Matches;

    [EnableCors("*", "*", "*")]
    public class MatchesController : ApiController
    {
        private readonly IQueryDispatcher queryDispatcher;

        public MatchesController(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
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
            IEnumerable<MatchResult> matches = queryDispatcher.Dispatch<AllMatchesQuery, IEnumerable<MatchResult>>(query);
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
            MatchResult result = queryDispatcher.Dispatch<MatchByIdQuery, MatchResult>(query);

            if (result == null)
            {
                return NotFound();
            }

            MatchResponseModel responseModel = Mapper.Map<MatchResponseModel>(result);

            return Ok(responseModel);
        }
    }
}