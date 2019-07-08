namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Odds;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class OddsManager : IOddsManager
    {
        private readonly IQueryHandler<EntityByKeyQuery<Odd>, Odd> oddByKeyHandler;
        private readonly ICommandHandler<UpdateOddCommand, string> updateOddHandler;
        private readonly ICommandHandler<CreateOddCommand, string> createOddHandler;

        public OddsManager(
            IQueryHandler<EntityByKeyQuery<Odd>, Odd> oddByKeyHandler,
            ICommandHandler<UpdateOddCommand, string> updateOddHandler,
            ICommandHandler<CreateOddCommand, string> createOddHandler)
        {
            this.oddByKeyHandler = oddByKeyHandler;
            this.updateOddHandler = updateOddHandler;
            this.createOddHandler = createOddHandler;
        }

        public void Manage(IEnumerable<OddFeedModel> feedModels, string marketId, string matchId)
        {
            foreach (var feedModel in feedModels)
            {
                EntityByKeyQuery<Odd> query = new EntityByKeyQuery<Odd>(feedModel.Id);
                Odd odd = oddByKeyHandler.Handle(query);

                if (odd != null)
                {
                    UpdateOddCommand updateCommand = new UpdateOddCommand()
                    {
                        Id = odd.Id,
                        IsActive = true,
                        Value = feedModel.Value,
                        IsSuspended = feedModel.IsSuspended
                    };

                    updateOddHandler.Handle(updateCommand);
                }
                else
                {
                    CreateOddCommand createCommand = new CreateOddCommand()
                    {
                        Key = feedModel.Id,
                        Header = feedModel.Header,
                        IsActive = true,
                        IsSuspended = feedModel.IsSuspended,
                        Name = feedModel.Name,
                        Value = feedModel.Value,
                        Rank = feedModel.Rank,
                        MarketId = marketId,
                        MatchId = matchId
                    };

                    createOddHandler.Handle(createCommand);
                }
            }
        }
    }
}