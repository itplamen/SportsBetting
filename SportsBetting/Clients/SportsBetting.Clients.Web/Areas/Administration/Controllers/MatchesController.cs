namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Matches;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class MatchesController : Controller
    {
        private readonly IMatchesService matchesService;

        public MatchesController(IMatchesService matchesService)
        {
            this.matchesService = matchesService;
        }

        public ActionResult Index()
        {
            IEnumerable<Match> matches = matchesService.AllWithDeleted();
            IEnumerable<MatchViewModel> matchViewModels = Mapper.Map<IEnumerable<MatchViewModel>>(matches);

            return View(matchViewModels);
        }
    }
}