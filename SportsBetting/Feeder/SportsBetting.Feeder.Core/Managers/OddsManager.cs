namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;

    using SportsBetting.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Data.Contracts;

    public class OddsManager : IOddsManager
    {
        private readonly IOddsService oddsService;
        private readonly IMapper<OddFeedModel, Odd> oddsMapper;

        public OddsManager(IOddsService oddsService, IMapper<OddFeedModel, Odd> oddsMapper)
        {
            this.oddsService = oddsService;
            this.oddsMapper = oddsMapper;
        }

        public void Manage(IEnumerable<OddFeedModel> feedModels, string marketId, string matchId)
        {
            foreach (var feedModel in feedModels)
            {
                Odd odd = oddsService.Get(feedModel.Id);
                Odd mappedOdd = oddsMapper.Map(feedModel);

                if (odd != null)
                {
                    oddsService.Update(odd.Id, mappedOdd);
                }
                else
                {
                    oddsService.Add(mappedOdd, marketId, matchId);   
                }
            }
        }
    }
}