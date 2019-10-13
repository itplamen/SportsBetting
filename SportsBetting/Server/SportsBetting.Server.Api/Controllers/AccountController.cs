namespace SportsBetting.Server.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Common.Validation;
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
        private readonly IQueryHandler<ValidateRegistrationQuery, ValidationResult> accountValidationHandler;

        public AccountController(
            ICommandHandler<CreateAccountCommand, string> createAccountHandler, 
            IQueryHandler<ValidateRegistrationQuery, ValidationResult> accountValidationHandler)
        {
            this.createAccountHandler = createAccountHandler;
            this.accountValidationHandler = accountValidationHandler;
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ValidateRegistrationQuery query = new ValidateRegistrationQuery(requestModel.Username, requestModel.Email);
            ValidationResult validationResult = accountValidationHandler.Handle(query);

            if (validationResult.HasErrors)
            {
                ModelState.AddModelError(validationResult.ErrorKey, validationResult.ErrorMessage);

                return BadRequest(ModelState);
            }

            CreateAccountCommand command = Mapper.Map<CreateAccountCommand>(requestModel);
            command.Role = AccontRole.User;

            RegisterResponseModel responseModel = new RegisterResponseModel();
            responseModel.Id = createAccountHandler.Handle(command);

            return Ok(responseModel);
        }
    }
}