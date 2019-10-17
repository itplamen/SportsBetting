namespace SportsBetting.Server.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Server.Api.Models.Account.Register;

    [EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;

        public AccountController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterRequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                CreateAccountCommand command = Mapper.Map<CreateAccountCommand>(requestModel);
                command.Role = AccontRole.User;

                ValidationResult validationResult = commandDispatcher.Validate(command);

                if (!validationResult.HasErrors)
                {
                    Account account = commandDispatcher.Dispatch<CreateAccountCommand, Account>(command);
                    RegisterResponseModel responseModel = Mapper.Map<RegisterResponseModel>(account);

                    return Ok(responseModel);
                }

                ModelState.AddModelError(validationResult.ErrorKey, validationResult.ErrorMessage);
            }

            return BadRequest(ModelState);
        }
    }
}