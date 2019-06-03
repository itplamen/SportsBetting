namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    
    public class OddsCache : BaseCache<Odd>
    {
        private const int REFRESH_INTERVAL = 1000 * 3;

        private readonly ICacheLoaderRepository<Odd> oddsRepository;

        public OddsCache(ICacheLoaderRepository<Odd> oddsRepository)
            : base(REFRESH_INTERVAL)
        {
            this.oddsRepository = oddsRepository;
        }

        public override void Load()
        {
            DateTime dateTime = DateTime.UtcNow.AddDays(-7);
            IEnumerable<Odd> odds = oddsRepository.Load(x => !x.IsDeleted && x.CreatedOn >= dateTime);

            foreach (var odd in odds)
            {
                Add(odd.Key, odd);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);
            IEnumerable<Odd> odds = oddsRepository.Load(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value >= dateTime);

            foreach (var odd in odds)
            {
                Update(odd.Key, odd);
            }
        }
    }
}