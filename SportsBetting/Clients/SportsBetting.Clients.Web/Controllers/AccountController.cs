namespace SportsBetting.Clients.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;

    using SportsBetting.Clients.Web.Models.Account;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Services.Data.Contracts;
    using SportsBetting.Services.Utils.Contracts;

    public class AccountController : Controller
    {
        private readonly IAccountsService accountsService;
        private readonly IEncryptionService encryptionService;
        private readonly IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler;

        public AccountController(
            IAccountsService accountsService, 
            IEncryptionService encryptionService,
            IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler)
        {
            this.accountsService = accountsService;
            this.encryptionService = encryptionService;
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

            if (accountsService.GetByEmail(viewModel.Email) != null)
            {
                ModelState.AddModelError(nameof(viewModel.Email), "A user with the same email has already been registered!");
            }

            if (ModelState.IsValid)
            {
                Account account = new Account()
                {
                    Username = viewModel.Username,
                    Password = encryptionService.Encrypt(viewModel.Password),
                    Email = viewModel.Email
                };

                accountsService.Add(account);
            }

            return View(viewModel);
        }
    }
}