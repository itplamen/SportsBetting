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
        private readonly IValidationHandler<CreateAccountCommand> validationHandler;
        private readonly ICommandHandler<CreateAccountCommand, Account> createAccountHandler;
        private readonly ICommandHandler<LoginAccountCommand, ValidationResult> loginAccountHandler;

        public AccountController(
            IValidationHandler<CreateAccountCommand> validationHandler,
            ICommandHandler<CreateAccountCommand, Account> createAccountHandler,
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
                    Account account = createAccountHandler.Handle(command);
                    RegisterResponseModel responseModel = new RegisterResponseModel();
                    responseModel.Id = account.Id;

                    return Ok(responseModel);
                }

                ModelState.AddModelError(validationResult.ErrorKey, validationResult.ErrorMessage);
            }

            return BadRequest(ModelState);
        }
    }
}