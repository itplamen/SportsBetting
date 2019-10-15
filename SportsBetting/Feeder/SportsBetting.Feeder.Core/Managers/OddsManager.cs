namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Odds;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class OddsManager : IOddsManager
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public OddsManager(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        public void Manage(IEnumerable<OddFeedModel> feedModels, string marketId, string matchId)
        {
            foreach (var feedModel in feedModels)
            {
                IEnumerable<int> keys = new List<int>() { feedModel.Key };
                EntitiesByKeyQuery<Odd> query = new EntitiesByKeyQuery<Odd>(keys);
                Odd odd = queryDispatcher.Dispatch<EntitiesByKeyQuery<Odd>, IEnumerable<Odd>>(query).FirstOrDefault();

                if (odd != null)
                {
                    UpdateOddCommand updateCommand = Mapper.Map<UpdateOddCommand>(feedModel);
                    updateCommand.Id = odd.Id;

                    commandDispatcher.Dispatch<UpdateOddCommand, string>(updateCommand);
                }
                else
                {
                    CreateOddCommand createCommand = Mapper.Map<CreateOddCommand>(feedModel);
                    createCommand.IsActive = true;
                    createCommand.MatchId = matchId;
                    createCommand.MarketId = marketId;

                    commandDispatcher.Dispatch<CreateOddCommand, string>(createCommand);
                }
            }
        }
    }
}