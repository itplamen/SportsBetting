namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Teams;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TeamsController : Controller
    {
        private readonly IQueryHandler<IEnumerable<Team>> allWithDeletedHandler;

        public TeamsController(IQueryHandler<IEnumerable<Team>> allWithDeletedHandler)
        {
            this.allWithDeletedHandler = allWithDeletedHandler;
        }

        public ActionResult Index()
        {
            IEnumerable<Team> teams = allWithDeletedHandler.Handle();
            IEnumerable<TeamViewModel> teamViewModels = Mapper.Map<IEnumerable<TeamViewModel>>(teams);

            return View(teamViewModels);
        }
    }
}