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
    using SportsBetting.Server.Api.Extensions;
    using SportsBetting.Server.Api.Models.Account;

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
    }
}