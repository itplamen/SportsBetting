namespace SportsBetting.Clients.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using SportsBetting.Clients.Web.Models.Home;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class HomeController : Controller
    {
        private readonly ITeamsService teamsService;
        private readonly IMatchesService matchesService;
        private readonly ICategoriesService categoriesService;
        private readonly ITournamentsService tournamentsService;

        public HomeController(
            ITeamsService teamsService,
            IMatchesService matchesService, 
            ICategoriesService categoriesService,
            ITournamentsService tournamentsService)
        {
            this.teamsService = teamsService;
            this.matchesService = matchesService;
            this.categoriesService = categoriesService;
            this.tournamentsService = tournamentsService;
        }

        public ActionResult Index()
        {
            IEnumerable<Match> matches = matchesService.AllActive()
                .OrderBy(x => x.StartTime);

            IEnumerable<string> categoryIds = matches.Select(x => x.CategoryId);
            IEnumerable<Category> categories = categoriesService.Get(categoryIds);

            IEnumerable<string> tournamentIds = matches.Select(x => x.TournamentId);
            IEnumerable<Tournament> tournaments = tournamentsService.Get(tournamentIds);

            List<string> teamIds = matches.Select(x => x.HomeTeamId).ToList();
            teamIds.AddRange(matches.Select(x => x.AwayTeamId));
            teamIds.Distinct();

            IEnumerable<Team> teams = teamsService.Get(teamIds);

            ICollection<GameViewModel> gameViewModels = new List<GameViewModel>();

            foreach (var match in matches)
            {
                GameViewModel gameViewModel = new GameViewModel()
                {
                    StartTime = match.StartTime,
                    Score = match.Score,
                    HomeTeam = teams.First(x => x.Id == match.HomeTeamId).Name,
                    AwayTeam = teams.First(x => x.Id == match.AwayTeamId).Name,
                    Category = categories.First(x => x.Id == match.CategoryId).Name,
                    Tournament = tournaments.First(x => x.Id == match.TournamentId).Name
                };

                gameViewModels.Add(gameViewModel);
            }

            return View(gameViewModels);
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