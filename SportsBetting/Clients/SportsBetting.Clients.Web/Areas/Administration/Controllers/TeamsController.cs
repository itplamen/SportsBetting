namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Teams;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        public ActionResult Index()
        {
            IEnumerable<Team> teams = teamsService.AllWithDeleted();
            IEnumerable<TeamViewModel> teamViewModels = Mapper.Map<IEnumerable<TeamViewModel>>(teams);

            return View(teamViewModels);
        }
    }
}