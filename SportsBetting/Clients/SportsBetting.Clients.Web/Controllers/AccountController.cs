namespace SportsBetting.Clients.Web.Controllers
{
    using System.Web.Mvc;

    using SportsBetting.Clients.Web.Models.Account;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;
    using SportsBetting.Services.Utils.Contracts;

    public class AccountController : Controller
    {
        private readonly IAccountsService accountsService;
        private readonly IEncryptionService encryptionService;

        public AccountController(IAccountsService accountsService, IEncryptionService encryptionService)
        {
            this.accountsService = accountsService;
            this.encryptionService = encryptionService;
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
            if (accountsService.GetByUsername(viewModel.Username) != null)
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