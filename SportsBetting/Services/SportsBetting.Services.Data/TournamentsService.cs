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

        public string Add(int key, string name, string categoryId)
        {
            Tournament tournament = new Tournament()
            {
                Key = key,
                Name = name,
                CategoryId = categoryId
            };

            tournamentsRepository.Add(tournament);

            return tournament.Id;
        }

        public Tournament Get(string name, string categoryId)
        {
            Tournament tournament = tournamentsRepository.All(x => x.Name == name && x.CategoryId == categoryId).FirstOrDefault();

            return tournament;
        }

        public IEnumerable<Tournament> AllWithDeleted()
        {
            IEnumerable<Tournament> tournaments = tournamentsRepository.All(x => true);

            return tournaments;
        }
    }
}