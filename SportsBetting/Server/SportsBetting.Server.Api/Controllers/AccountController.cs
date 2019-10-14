namespace SportsBetting.Server.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts;
    using SportsBetting.Handlers.Commands.Accounts.Results;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Server.Api.Models.Account.Register;

    [EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        private readonly IValidationHandler<CreateAccountCommand> validationHandler;
        private readonly ICommandHandler<CreateAccountCommand, AccountResult> createAccountHandler;
        private readonly ICommandHandler<LoginAccountCommand, ValidationResult> loginAccountHandler;

        public AccountController(
            IValidationHandler<CreateAccountCommand> validationHandler,
            ICommandHandler<CreateAccountCommand, AccountResult> createAccountHandler,
            ICommandHandler<LoginAccountCommand, ValidationResult> loginAccountHandler)
        {
            this.validationHandler = validationHandler;
            this.createAccountHandler = createAccountHandler;
            this.loginAccountHandler = loginAccountHandler;
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterRequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                CreateAccountCommand command = Mapper.Map<CreateAccountCommand>(requestModel);
                command.Role = AccontRole.User;

                ValidationResult validationResult = validationHandler.Validate(command);

                if (!validationResult.HasErrors)
                {
                    RegisterResponseModel responseModel = new RegisterResponseModel();
                    responseModel.Id = createAccountHandler.Handle(command).Account.Id;

                    return Ok(responseModel);
                }

                ModelState.AddModelError(validationResult.ErrorKey, validationResult.ErrorMessage);
            }

            return BadRequest(ModelState);
        }
    }
}