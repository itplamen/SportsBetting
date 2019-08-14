namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Markets;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MarketsManager : IMarketsManager
    {
        private readonly ICommandHandler<CreateMarketCommand, string> createMarketHandler;
        private readonly IQueryHandler<EntitiesByKeyQuery<Market>, IEnumerable<Market>> marketByKeyHandler;

        public MarketsManager(
            ICommandHandler<CreateMarketCommand, string> createMarketHandler,
            IQueryHandler<EntitiesByKeyQuery<Market>, IEnumerable<Market>> marketByKeyHandler)
        {
            this.createMarketHandler = createMarketHandler;
            this.marketByKeyHandler = marketByKeyHandler;
        }

        public string Manage(MarketFeedModel feedModel, string matchId)
        {
            IEnumerable<int> keys = new List<int>() { feedModel.Key };
            EntitiesByKeyQuery<Market> query = new EntitiesByKeyQuery<Market>(keys);
            Market market = marketByKeyHandler.Handle(query).FirstOrDefault();

            if (market != null)
            {
                return market.Id;
            }

            CreateMarketCommand command = Mapper.Map<CreateMarketCommand>(feedModel);
            command.MatchId = matchId;

            return createMarketHandler.Handle(command);
        }
    }
}