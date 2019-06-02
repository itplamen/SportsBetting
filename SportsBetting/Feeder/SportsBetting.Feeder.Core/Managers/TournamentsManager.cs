namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;

    public class TournamentsManager : ITournamentsManager
    {
        private readonly IRepository<Tournament> tournamentsRepository;

        public TournamentsManager(IRepository<Tournament> tournamentsRepository)
        {
            this.tournamentsRepository = tournamentsRepository;
        }

        public string Manage(TournamentFeedModel feedModel, string categoryId)
        {
            Tournament tournament = tournamentsRepository.All(x => x.Name == feedModel.Name && x.CategoryId == categoryId).FirstOrDefault();

            if (tournament == null)
            {
                return Add(feedModel, categoryId);
            }

            return tournament.Id;
        }

        private string Add(TournamentFeedModel feedModel, string categoryId)
        {
            Tournament tournament = new Tournament()
            {
                Key = feedModel.Id,
                Name = feedModel.Name,
                CategoryId = categoryId
            };

            tournamentsRepository.Add(tournament);

            return tournament.Id;
        }
    }
}