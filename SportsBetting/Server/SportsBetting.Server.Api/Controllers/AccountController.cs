namespace SportsBetting.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Auth.Commands;
    using SportsBetting.Handlers.Commands.Common.Commands;
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
                UsernameCommand uniqueUsernameCommand = new UsernameCommand(requestModel.Username);
                IEnumerable<ValidationResult> validations = commandDispatcher.Validate(uniqueUsernameCommand);

                ModelState.AddModelErrors(validations);

                if (ModelState.IsValid)
                {
                    AccountCommand accountCommand = new AccountCommand(requestModel.Username, requestModel.Password);
                    Account account = commandDispatcher.Dispatch<AccountCommand, Account>(accountCommand);

                    AuthCommand authCommand = new AuthCommand(account.Id, false);
                    Authentication authentication = commandDispatcher.Dispatch<AuthCommand, Authentication>(authCommand);

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

                    AccountCommand loginCommand = new AccountCommand(requestModel.Username, requestModel.Password);
                    Authentication authentication = commandDispatcher.Dispatch<AccountCommand, Authentication>(loginCommand);

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