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
    using SportsBetting.Handlers.Queries.Common.Queries;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MarketsManager : IMarketsManager
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public MarketsManager(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        public string Manage(MarketFeedModel feedModel, string matchId)
        {
            IEnumerable<int> keys = new List<int>() { feedModel.Key };
            EntitiesByKeyQuery<Market> query = new EntitiesByKeyQuery<Market>(keys);
            Market market = queryDispatcher.Dispatch<EntitiesByKeyQuery<Market>, IEnumerable<Market>>(query).FirstOrDefault();

            if (market != null)
            {
                return market.Id;
            }

            CreateMarketCommand command = Mapper.Map<CreateMarketCommand>(feedModel);
            command.MatchId = matchId;

            return commandDispatcher.Dispatch<CreateMarketCommand, string>(command);
        }
    }
}