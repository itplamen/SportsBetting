namespace SportsBetting.Server.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Collections.Generic;

    using AutoMapper;

    using SportsBetting.Common.Results;
    using SportsBetting.Handlers.Commands.Auth.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Server.Api.Extensions;
    using SportsBetting.Server.Api.Models.Auth;
    using SportsBetting.Server.Api.Models.Account;
    using SportsBetting.Handlers.Commands.Common.Commands;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Data.Models;

    [EnableCors("*", "*", "*")]
    public class AuthController : ApiController
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public AuthController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
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