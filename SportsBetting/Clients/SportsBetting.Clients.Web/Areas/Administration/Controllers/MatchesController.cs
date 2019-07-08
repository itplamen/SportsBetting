namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Matches;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MatchesController : Controller
    {
        private readonly IQueryHandler<IEnumerable<Match>> allWithDeletedHandler;

        public MatchesController(IQueryHandler<IEnumerable<Match>> allWithDeletedHandler)
        {
            this.allWithDeletedHandler = allWithDeletedHandler;
        }

        public ActionResult Index()
        {
            IEnumerable<Match> matches = allWithDeletedHandler.Handle();
            IEnumerable<MatchViewModel> matchViewModels = Mapper.Map<IEnumerable<MatchViewModel>>(matches);

            return View(matchViewModels);
        }
    }
}