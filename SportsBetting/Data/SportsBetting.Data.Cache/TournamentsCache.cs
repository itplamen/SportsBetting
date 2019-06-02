namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class TournamentsCache : BaseCache<Tournament>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

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
                Add(tournament.Key, tournament);
            }
        }

        public override void Refresh()
        {
            IEnumerable<Tournament> tournaments = tournamentsRepository.Load(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value.AddMilliseconds(REFRESH_INTERVAL) >= DateTime.UtcNow);

            foreach (var tournament in tournaments)
            {
                Update(tournament.Key, tournament);
            }
        }
    }
}