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
        private readonly ICommandHandler<UpdateOddCommand, string> updateOddHandler;
        private readonly ICommandHandler<CreateOddCommand, string> createOddHandler;
        private readonly IQueryHandler<EntitiesByKeyQuery<Odd>, IEnumerable<Odd>> oddByKeyHandler;

        public OddsManager(
            ICommandHandler<UpdateOddCommand, string> updateOddHandler,
            ICommandHandler<CreateOddCommand, string> createOddHandler,
            IQueryHandler<EntitiesByKeyQuery<Odd>, IEnumerable<Odd>> oddByKeyHandler)
        {
            this.oddByKeyHandler = oddByKeyHandler;
            this.updateOddHandler = updateOddHandler;
            this.createOddHandler = createOddHandler;
        }

        public void Manage(IEnumerable<OddFeedModel> feedModels, string marketId, string matchId)
        {
            foreach (var feedModel in feedModels)
            {
                IEnumerable<int> keys = new List<int>() { feedModel.Key };
                EntitiesByKeyQuery<Odd> query = new EntitiesByKeyQuery<Odd>(keys);
                Odd odd = oddByKeyHandler.Handle(query).FirstOrDefault();

                if (odd != null)
                {
                    UpdateOddCommand updateCommand = Mapper.Map<UpdateOddCommand>(feedModel);
                    updateCommand.Id = odd.Id;

                    updateOddHandler.Handle(updateCommand);
                }
                else
                {
                    CreateOddCommand createCommand = Mapper.Map<CreateOddCommand>(feedModel);
                    createCommand.IsActive = true;
                    createCommand.MatchId = matchId;
                    createCommand.MarketId = marketId;

                    createOddHandler.Handle(createCommand);
                }
            }
        }
    }
}