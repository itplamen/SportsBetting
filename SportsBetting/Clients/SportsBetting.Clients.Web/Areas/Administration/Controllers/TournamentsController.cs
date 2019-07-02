namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Tournaments;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class TournamentsController : Controller
    {
        private readonly ITournamentsService tournamentsService;

        public TournamentsController(ITournamentsService tournamentsService)
        {
            this.tournamentsService = tournamentsService;
        }

        public ActionResult Index()
        {
            IEnumerable<Tournament> tournaments = tournamentsService.All();
            IEnumerable<TournamentViewModel> tournamentViewModels = Mapper.Map<IEnumerable<TournamentViewModel>>(tournaments);

            return View(tournamentViewModels);
        }
    }
}