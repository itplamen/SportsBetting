namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;

    using SportsBetting.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Odds;
    using SportsBetting.Services.Data.Contracts;

    public class OddsManager : IOddsManager
    {
        private readonly IOddsService oddsService;
        private readonly IMapper<OddFeedModel, Odd> oddsMapper;
        private readonly IQueryHandler<OddByKeyQuery, Odd> oddByKeyHandler;

        public OddsManager(
            IOddsService oddsService, 
            IMapper<OddFeedModel, Odd> oddsMapper,
            IQueryHandler<OddByKeyQuery, Odd> oddByKeyHandler)
        {
            this.oddsService = oddsService;
            this.oddsMapper = oddsMapper;
            this.oddByKeyHandler = oddByKeyHandler;
        }

        public void Manage(IEnumerable<OddFeedModel> feedModels, string marketId, string matchId)
        {
            foreach (var feedModel in feedModels)
            {
                OddByKeyQuery query = new OddByKeyQuery(feedModel.Id);
                Odd odd = oddByKeyHandler.Handle(query);

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