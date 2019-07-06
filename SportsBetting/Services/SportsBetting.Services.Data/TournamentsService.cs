namespace SportsBetting.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class TournamentsService : ITournamentsService
    {
        private readonly IRepository<Tournament> tournamentsRepository;

        public TournamentsService(IRepository<Tournament> tournamentsRepository)
        {
            this.tournamentsRepository = tournamentsRepository;
        }

        public IEnumerable<Tournament> Get(IEnumerable<string> tournamentIds)
        {
            IEnumerable<Tournament> tournaments = tournamentsRepository.All(x => !x.IsDeleted && tournamentIds.Contains(x.Id));

            return tournaments;
        }

        public IEnumerable<Tournament> AllWithDeleted()
        {
            IEnumerable<Tournament> tournaments = tournamentsRepository.All(x => true);

            return tournaments;
        }
    }
}