namespace SportsBetting.Data.Initialize
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    internal sealed class SeedData
    {
        internal SeedData()
        {
            this.Sports = new List<Sport>();
            this.SeedSports();
        }

        internal ICollection<Sport> Sports { get; private set; }

        private void SeedSports()
        {
            Sport sport = new Sport()
            {
                Key = 1,
                Name = "eSports",
                CreatedOn = DateTime.UtcNow
            };

            Sports.Add(sport);
        }
    }
}