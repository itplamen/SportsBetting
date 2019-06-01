namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Core.Contracts.Mappers;
    using SportsBetting.Feeder.Models;

    public class OddsManager : IOddsManager
    {
        private readonly IRepository<Odd> oddsRepository;
        private readonly IMapper<OddFeedModel, Odd> oddsMapper;

        public OddsManager(IRepository<Odd> oddsRepository, IMapper<OddFeedModel, Odd> oddsMapper)
        {
            this.oddsRepository = oddsRepository;
            this.oddsMapper = oddsMapper;
        }

        public void Manage(IEnumerable<OddFeedModel> feedModels, string marketId, string matchId)
        {
            foreach (var feedModel in feedModels)
            {
                Odd odd = oddsRepository.All(x => x.Key == feedModel.Id).FirstOrDefault();
                Odd mappedOdd = oddsMapper.Map(feedModel);

                if (odd == null)
                {
                    Add(mappedOdd, marketId, matchId);
                }
                else
                {
                    Update(mappedOdd);
                }
            }
        }

        private void Add(Odd odd, string marketId, string matchId)
        {
            odd.MarketId = marketId;
            odd.MatchId = matchId;

            oddsRepository.Add(odd);
        }

        private void Update(Odd odd)
        {
            Odd oddToUpdate = oddsRepository.All(x => x.Key == odd.Key).FirstOrDefault();
            oddToUpdate.Value = odd.Value;
            oddToUpdate.IsActive = true;
            oddToUpdate.IsSuspended = odd.IsSuspended;
            oddToUpdate.ResultStatus = odd.ResultStatus;

            oddsRepository.Update(oddToUpdate);
        }
    }
}