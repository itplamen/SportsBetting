namespace SportsBetting.Clients.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;

    using SportsBetting.Clients.Web.Models.Account;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Services.Utils.Contracts;

    public class AccountController : Controller
    {
        private readonly IEncryptionService encryptionService;
        private readonly IQueryHandler<AccountByEmailQuery, Account> accountByEmailHandler;
        private readonly ICommandHandler<CreateAccountCommand, string> createAccountHandler;
        private readonly IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler;

        public AccountController(
            IEncryptionService encryptionService,
            IQueryHandler<AccountByEmailQuery, Account> accountByEmailHandler,
            ICommandHandler<CreateAccountCommand, string> createAccountHandler,
            IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler)
        {
            this.encryptionService = encryptionService;
            this.accountByEmailHandler = accountByEmailHandler;
            this.createAccountHandler = createAccountHandler;
            this.accountByUsernameHandler = accountByUsernameHandler;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(viewModel.Username, false);
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            AccountByUsernameQuery accountByUsernameQuery = new AccountByUsernameQuery(viewModel.Username);

            if (accountByUsernameHandler.Handle(accountByUsernameQuery) != null)
            {
                ModelState.AddModelError(nameof(viewModel.Username), "A user with the same username has already been registered!");
            }

            AccountByEmailQuery accountByEmailQuery = new AccountByEmailQuery(viewModel.Email);

            if (accountByEmailHandler.Handle(accountByEmailQuery) != null)
            {
                ModelState.AddModelError(nameof(viewModel.Email), "A user with the same email has already been registered!");
            }

            if (ModelState.IsValid)
            {
                CreateAccountCommand command = new CreateAccountCommand()
                {
                    Role = AccontRole.User,
                    Email = viewModel.Email,
                    Username = viewModel.Username,
                    Password = encryptionService.Encrypt(viewModel.Password),
                };

                createAccountHandler.Handle(command);
            }

            return View(viewModel);
        }
    }
}