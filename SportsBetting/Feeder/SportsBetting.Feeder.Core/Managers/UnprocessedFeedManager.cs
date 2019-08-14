namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Models;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Feeder.Core.Contracts;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Common;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class UnprocessedFeedManager : IUnprocessedFeedManager
    {
        private readonly ICommandHandler<HideEntitiesCommand<Odd>> oddsCommandHandler;
        private readonly ICommandHandler<HideEntitiesCommand<Match>> matchesCommandHandler;
        private readonly ICommandHandler<HideEntitiesCommand<Market>> marketsCommandHandler;
        private readonly IQueryHandler<EntitiesByKeyQuery<Odd>, IEnumerable<Odd>> oddsQueryHandler;
        private readonly IQueryHandler<EntitiesByKeyQuery<Match>, IEnumerable<Match>> matchesQueryHandler;
        private readonly IQueryHandler<EntitiesByKeyQuery<Market>, IEnumerable<Market>> marketsQueryHandler;

        public UnprocessedFeedManager(
            ICommandHandler<HideEntitiesCommand<Odd>> oddsCommandHandler,
            ICommandHandler<HideEntitiesCommand<Match>> matchesCommandHandler,
            ICommandHandler<HideEntitiesCommand<Market>> marketsCommandHandler,
            IQueryHandler<EntitiesByKeyQuery<Odd>, IEnumerable<Odd>> oddsQueryHandler,
            IQueryHandler<EntitiesByKeyQuery<Match>, IEnumerable<Match>> matchesQueryHandler,
            IQueryHandler<EntitiesByKeyQuery<Market>, IEnumerable<Market>> marketsQueryHandler)
        {
            this.oddsCommandHandler = oddsCommandHandler;
            this.matchesCommandHandler = matchesCommandHandler;
            this.marketsCommandHandler = marketsCommandHandler;
            this.oddsQueryHandler = oddsQueryHandler;
            this.matchesQueryHandler = matchesQueryHandler;
            this.marketsQueryHandler = marketsQueryHandler;
        }

        public void Manage(IEnumerable<MatchFeedModel> processedFeed)
        {
            IEnumerable<int> matchKeys = processedFeed.Select(x => x.Key);
            HideUnprocessedEntities(matchKeys, matchesCommandHandler, matchesQueryHandler);

            IEnumerable<int> marketKeys = processedFeed.SelectMany(x => x.Markets.Select(y => y.Key));
            HideUnprocessedEntities(marketKeys, marketsCommandHandler, marketsQueryHandler);

            IEnumerable<int> oddKeys = processedFeed.SelectMany(x => x.Markets.SelectMany(y => y.Odds.Select(z => z.Key)));
            HideUnprocessedEntities(oddKeys, oddsCommandHandler, oddsQueryHandler);
        }

        private void HideUnprocessedEntities<TEntity>(
            IEnumerable<int> entityKeys,
            ICommandHandler<HideEntitiesCommand<TEntity>> commandHandler,
            IQueryHandler<EntitiesByKeyQuery<TEntity>, IEnumerable<TEntity>> queryHandler)
            where TEntity : BaseModel
        {
            EntitiesByKeyQuery<TEntity> entitiesQuery = new EntitiesByKeyQuery<TEntity>(entityKeys);
            IEnumerable<TEntity> entities = queryHandler.Handle(entitiesQuery);

            HideEntitiesCommand<TEntity> entitiesCommand = new HideEntitiesCommand<TEntity>(entities);
            commandHandler.Handle(entitiesCommand);
        }
    }
}