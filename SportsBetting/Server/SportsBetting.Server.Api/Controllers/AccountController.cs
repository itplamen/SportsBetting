using AutoMapper;
using SportsBetting.Data.Models;
using SportsBetting.Handlers.Commands.Accounts;
using SportsBetting.Handlers.Commands.Contracts;
using SportsBetting.Handlers.Queries.Accounts;
using SportsBetting.Handlers.Queries.Contracts;
using SportsBetting.Server.Api.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SportsBetting.Server.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        private readonly IQueryHandler<AccountByEmailQuery, Account> accountByEmailHandler;
        private readonly ICommandHandler<CreateAccountCommand, string> createAccountHandler;
        private readonly IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler;

        public AccountController(
            IQueryHandler<AccountByEmailQuery, Account> accountByEmailHandler,
            ICommandHandler<CreateAccountCommand, string> createAccountHandler,
            IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler)
        {
            this.accountByEmailHandler = accountByEmailHandler;
            this.createAccountHandler = createAccountHandler;
            this.accountByUsernameHandler = accountByUsernameHandler;
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterRequestModel requestModel)
        {
            AccountByUsernameQuery accountByUsernameQuery = new AccountByUsernameQuery(requestModel.Username);

            if (accountByUsernameHandler.Handle(accountByUsernameQuery) != null)
            {
                ModelState.AddModelError(nameof(requestModel.Username), "A user with the same username has already been registered!");
            }

            AccountByEmailQuery accountByEmailQuery = new AccountByEmailQuery(requestModel.Email);

            if (accountByEmailHandler.Handle(accountByEmailQuery) != null)
            {
                ModelState.AddModelError(nameof(requestModel.Email), "A user with the same email has already been registered!");
            }

            if (ModelState.IsValid)
            {
                CreateAccountCommand command = Mapper.Map<CreateAccountCommand>(requestModel);
                command.Role = AccontRole.User;

                createAccountHandler.Handle(command);

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}