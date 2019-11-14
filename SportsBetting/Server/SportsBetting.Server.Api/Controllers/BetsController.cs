namespace SportsBetting.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Common.Results;
    using SportsBetting.Handlers.Commands.Bets.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Server.Api.Extensions;
    using SportsBetting.Server.Api.Models.Bets;

    [EnableCors("*", "*", "*")]
    public class BetsController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;

        public BetsController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public IHttpActionResult Post(BetRequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                PlaceBetCommand placeBetCommand = Mapper.Map<PlaceBetCommand>(requestModel);
                IEnumerable<ValidationResult> validations = commandDispatcher.Validate(placeBetCommand);

                ModelState.AddModelErrors(validations);

                if (ModelState.IsValid)
                {
                    BetResponseModel responseModel = new BetResponseModel();
                    responseModel.TicketId = commandDispatcher.Dispatch<PlaceBetCommand, string>(placeBetCommand);

                    return Ok(responseModel);
                }
            }

            return BadRequest(ModelState);
        }
    }
}