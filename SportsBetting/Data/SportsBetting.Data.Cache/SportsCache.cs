namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class SportsCache : BaseCache<Sport>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        private readonly ICacheLoaderRepository<Sport> sportsRepository;

        public SportsCache(ICacheLoaderRepository<Sport> sportsRepository)
        {
            this.sportsRepository = sportsRepository;
        }

        public override void Load()
        {
            IEnumerable<Sport> sports = sportsRepository.Load(x => !x.IsDeleted);

            foreach (var sport in sports)
            {
                Add(sport.Key, sport);
            }
        }

        public override void Refresh()
        {
            IEnumerable<Sport> sports = sportsRepository.Load(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value.AddMilliseconds(REFRESH_INTERVAL) >= DateTime.UtcNow);

            foreach (var sport in sports)
            {
                Update(sport.Key, sport);
            }
        }
    }
}