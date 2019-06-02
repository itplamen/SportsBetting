namespace SportsBetting.Data.Cache
{
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class TournamentsCache : BaseCache<Tournament>
    {
        private readonly ICacheLoaderRepository<Tournament> tournamentsRepository;

        public TournamentsCache(ICacheLoaderRepository<Tournament> tournamentsRepository)
        {
            this.tournamentsRepository = tournamentsRepository;
        }

        public override void Load()
        {
            IEnumerable<Tournament> tournaments = tournamentsRepository.Load(x => !x.IsDeleted);

            foreach (var tournament in tournaments)
            {
                Cache[tournament.Key] = tournament;
            }
        }
    }
}