namespace SportsBetting.Server.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Server.Api.Models.Account.Register;

    [EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        private readonly ICommandHandler<CreateAccountCommand, string> createAccountHandler;
        private readonly ICommandHandler<LoginAccountCommand, ValidationResult> loginAccountHandler;
        private readonly IQueryHandler<ValidateRegistrationQuery, ValidationResult> accountValidationHandler;

        public AccountController(
            ICommandHandler<CreateAccountCommand, string> createAccountHandler,
            ICommandHandler<LoginAccountCommand, ValidationResult> loginAccountHandler,
            IQueryHandler<ValidateRegistrationQuery, ValidationResult> accountValidationHandler)
        {
            this.createAccountHandler = createAccountHandler;
            this.loginAccountHandler = loginAccountHandler;
            this.accountValidationHandler = accountValidationHandler;
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterRequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                ValidateRegistrationQuery query = new ValidateRegistrationQuery(requestModel.Username, requestModel.Email);
                ValidationResult validationResult = accountValidationHandler.Handle(query);

                if (!validationResult.HasErrors)
                {
                    CreateAccountCommand command = Mapper.Map<CreateAccountCommand>(requestModel);
                    command.Role = AccontRole.User;

                    RegisterResponseModel responseModel = new RegisterResponseModel();
                    responseModel.Id = createAccountHandler.Handle(command);

                    return Ok(responseModel);
                }

                ModelState.AddModelError(validationResult.ErrorKey, validationResult.ErrorMessage);
            }

            return BadRequest(ModelState);
        }
    }
}