namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    
    public class OddsCache : BaseCache<Odd>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        private readonly ICacheLoaderRepository<Odd> oddsRepository;

        public OddsCache(ICacheLoaderRepository<Odd> oddsRepository)
            : base(REFRESH_INTERVAL)
        {
            this.oddsRepository = oddsRepository;
        }

        public override void Load()
        {
            IEnumerable<Odd> odds = oddsRepository.Load(x => !x.IsDeleted && x.CreatedOn.AddDays(7) >= DateTime.UtcNow);

            foreach (var odd in odds)
            {
                Add(odd.Key, odd);
            }
        }

        public override void Refresh()
        {
            IEnumerable<Odd> odds = oddsRepository.Load(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value.AddMilliseconds(REFRESH_INTERVAL) >= DateTime.UtcNow);

            foreach (var odd in odds)
            {
                Update(odd.Key, odd);
            }
        }
    }
}