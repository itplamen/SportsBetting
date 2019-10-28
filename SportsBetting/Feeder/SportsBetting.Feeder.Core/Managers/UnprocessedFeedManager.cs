namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Models;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Common;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Common.Queries;
    using SportsBetting.Handlers.Queries.Contracts;

    public class UnprocessedFeedManager : IUnprocessedFeedManager
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public UnprocessedFeedManager(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        public void Manage(IEnumerable<MatchFeedModel> processedFeed)
        {
            IEnumerable<int> matchKeys = processedFeed.Select(x => x.Key);
            DeleteUnprocessedEntities<Match>(matchKeys);

            IEnumerable<int> marketKeys = processedFeed.SelectMany(x => x.Markets.Select(y => y.Key));
            DeleteUnprocessedEntities<Market>(marketKeys);

            IEnumerable<int> oddKeys = processedFeed.SelectMany(x => x.Markets.SelectMany(y => y.Odds.Select(z => z.Key)));
            DeleteUnprocessedEntities<Odd>(oddKeys);
        }

        private void DeleteUnprocessedEntities<TEntity>(IEnumerable<int> entityKeys)
            where TEntity : BaseModel
        {
            EntitiesByKeyQuery<TEntity> entitiesQuery = new EntitiesByKeyQuery<TEntity>(entityKeys, x => !entityKeys.Contains(x.Key));
            IEnumerable<TEntity> entities = queryDispatcher.Dispatch<EntitiesByKeyQuery<TEntity>,IEnumerable<TEntity>>(entitiesQuery);

            DeleteEntitiesCommand<TEntity> entitiesCommand = new DeleteEntitiesCommand<TEntity>(entities);
            commandDispatcher.Dispatch(entitiesCommand);
        }
    }
}