namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Accounts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountsController : Controller
    {
        private readonly IQueryHandler<IEnumerable<Account>> allWithDeletedHandler;

        public AccountsController(IQueryHandler<IEnumerable<Account>> allWithDeletedHandler)
        {
            this.allWithDeletedHandler = allWithDeletedHandler;
        }

        public ActionResult Index()
        {
            IEnumerable<Account> accounts = allWithDeletedHandler.Handle();
            IEnumerable<AccountViewModel> accountViewModels = Mapper.Map<IEnumerable<AccountViewModel>>(accounts);

            return View(accountViewModels);
        }
    }
}