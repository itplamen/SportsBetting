namespace SportsBetting.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Server.Api.Extensions;
    using SportsBetting.Server.Api.Models.Account;

    [EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public AccountController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterRequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                AccountCommand registerCommand = Mapper.Map<AccountCommand>(requestModel);
                IEnumerable<ValidationResult> validations = commandDispatcher.Validate(registerCommand);

                ModelState.AddModelErrors(validations);

                if (ModelState.IsValid)
                {
                    Account account = commandDispatcher.Dispatch<AccountCommand, Account>(registerCommand);
                    LoginCommand authCommand = new LoginCommand(account.Id, false);

                    Authentication authentication = commandDispatcher.Dispatch<LoginCommand, Authentication>(authCommand);

                    AccountResponseModel responseModel = Mapper
                        .Map<AccountResponseModel>(authentication)
                        .Map(account);

                    return Ok(responseModel);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult Login(LoginRequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                AccountCommand accountCommand = Mapper.Map<AccountCommand>(requestModel);
                IEnumerable<ValidationResult> validations = commandDispatcher.Validate(accountCommand);

                ModelState.AddModelErrors(validations);

                if (ModelState.IsValid)
                {
                    AccountByUsernameQuery query = new AccountByUsernameQuery(requestModel.Username);
                    Account account = queryDispatcher.Dispatch<AccountByUsernameQuery, Account>(query);

                    LoginCommand loginCommand = new LoginCommand(account.Id, requestModel.RememberMe);
                    Authentication authentication = commandDispatcher.Dispatch<LoginCommand, Authentication>(loginCommand);

                    AccountResponseModel responseModel = Mapper
                        .Map<AccountResponseModel>(authentication)
                        .Map(account);

                    return Ok(responseModel);
                }
            }

            return BadRequest(ModelState);
        }
    }
}