namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Tournaments;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TournamentsController : Controller
    {
        private readonly IQueryHandler<IEnumerable<Tournament>> allWithDeletedHandler;

        public TournamentsController(IQueryHandler<IEnumerable<Tournament>> allWithDeletedHandler)
        {
            this.allWithDeletedHandler = allWithDeletedHandler;
        }

        public ActionResult Index()
        {
            IEnumerable<Tournament> tournaments = allWithDeletedHandler.Handle();
            IEnumerable<TournamentViewModel> tournamentViewModels = Mapper.Map<IEnumerable<TournamentViewModel>>(tournaments);

            return View(tournamentViewModels);
        }
    }
}