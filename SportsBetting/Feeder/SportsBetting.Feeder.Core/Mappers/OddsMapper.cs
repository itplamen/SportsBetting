namespace SportsBetting.Feeder.Core.Mappers
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Mappers;
    using SportsBetting.Feeder.Models;

    public class OddsMapper : IMapper<OddFeedModel, Odd>
    {
        public Odd Map(OddFeedModel from)
        {
            Odd odd = new Odd()
            {
                Key = from.Id,
                Name = from.Name,
                Rank = from.Rank,
                Value = from.Value,
                Header = from.Header,
                IsSuspended = from.IsSuspended,
                ResultStatus = Map(from.ResultStatus)
            };

            return odd;
        }

        public IEnumerable<Odd> Map(IEnumerable<OddFeedModel> from)
        {
            ICollection<Odd> odds = new List<Odd>();

            foreach (var oddFeed in from)
            {
                odds.Add(Map(oddFeed));
            }

            return odds;
        }

        private OddResultStatus Map(OddResultFeedStatus from)
        {
            if (from == OddResultFeedStatus.Win)
            {
                return OddResultStatus.Win;
            }

            if (from == OddResultFeedStatus.Loss)
            {
                return OddResultStatus.Loss;
            }

            return OddResultStatus.NotResulted;
        }
    }
}