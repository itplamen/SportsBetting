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
                RegisterCommand registerCommand = Mapper.Map<RegisterCommand>(requestModel);
                IEnumerable<ValidationResult> validations = commandDispatcher.Validate(registerCommand);

                ModelState.AddModelErrors(validations);

                if (ModelState.IsValid)
                {
                    Account account = commandDispatcher.Dispatch<RegisterCommand, Account>(registerCommand);
                    AuthenticateAccountCommand authCommand = new AuthenticateAccountCommand(account.Id, false);

                    Authentication authentication = commandDispatcher.Dispatch<AuthenticateAccountCommand, Authentication>(authCommand);

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
                LoginAccountCommand loginCommand = Mapper.Map<LoginAccountCommand>(requestModel);
                IEnumerable<ValidationResult> validations = commandDispatcher.Validate(loginCommand);

                ModelState.AddModelErrors(validations);

                if (ModelState.IsValid)
                {
                    AccountByExpressionQuery query = new AccountByExpressionQuery(x => x.Username == requestModel.Username);
                    Account account = queryDispatcher.Dispatch<AccountByExpressionQuery, Account>(query);

                    AuthenticateAccountCommand authCommand = new AuthenticateAccountCommand(account.Id, requestModel.RememberMe);
                    Authentication authentication = commandDispatcher.Dispatch<AuthenticateAccountCommand, Authentication>(authCommand);

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