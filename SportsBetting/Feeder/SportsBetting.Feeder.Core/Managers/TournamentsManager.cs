namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Data.Contracts;

    public class TournamentsManager : ITournamentsManager
    {
        private readonly ITournamentsService tournamentsService;

        public TournamentsManager(ITournamentsService tournamentsService)
        {
            this.tournamentsService = tournamentsService;
        }

        public string Manage(TournamentFeedModel feedModel, string categoryId)
        {
            Tournament tournament = tournamentsService.Get(feedModel.Name, categoryId);

            if (tournament != null)
            {
                return tournament.Id;
            }

            return tournamentsService.Add(feedModel.Id, feedModel.Name, categoryId);
        }       
    }
}