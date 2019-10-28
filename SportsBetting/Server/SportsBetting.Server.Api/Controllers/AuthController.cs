namespace SportsBetting.Server.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Collections.Generic;

    using SportsBetting.Common.Results;
    using SportsBetting.Handlers.Commands.Auth.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Server.Api.Extensions;
    using SportsBetting.Server.Api.Models.Auth;

    [EnableCors("*", "*", "*")]
    public class AuthController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;

        public AuthController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public IHttpActionResult Logout(LogoutRequestModel requestModel)
        {
            LogoutCommand logoutCommand = new LogoutCommand(requestModel.LoginToken);
            IEnumerable<ValidationResult> validations = commandDispatcher.Validate(logoutCommand);

            ModelState.AddModelErrors(validations);

            if (ModelState.IsValid)
            {
                commandDispatcher.Dispatch(logoutCommand);

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}