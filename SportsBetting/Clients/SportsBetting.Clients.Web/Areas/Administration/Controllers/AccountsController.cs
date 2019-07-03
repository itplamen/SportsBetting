namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Accounts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class AccountsController : Controller
    {
        private readonly IAccountsService accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        public ActionResult Index()
        {
            IEnumerable<Account> accounts = accountsService.AllWithDeleted();
            IEnumerable<AccountViewModel> accountViewModels = Mapper.Map<IEnumerable<AccountViewModel>>(accounts);

            return View(accountViewModels);
        }
    }
}