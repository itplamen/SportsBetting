namespace SportsBetting.Clients.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Models.Home;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches;

    public class HomeController : Controller
    {
        private readonly IQueryHandler<UpcomingMatchesQuery, IEnumerable<UpcomingMatchesResult>> upcomingMatchesHandler;

        public HomeController(IQueryHandler<UpcomingMatchesQuery, IEnumerable<UpcomingMatchesResult>> upcomingMatchesHandler)
        {
            this.upcomingMatchesHandler = upcomingMatchesHandler;
        }

        public ActionResult Index(SelectGamesViewModel viewModel)
        {
            UpcomingMatchesQuery query = Mapper.Map<UpcomingMatchesQuery>(viewModel);
            IEnumerable<UpcomingMatchesResult> upcomingMatches = upcomingMatchesHandler.Handle(query);
            IEnumerable<GameViewModel> viewModels = Mapper.Map<IEnumerable<GameViewModel>>(upcomingMatches);

            return View(viewModels);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}