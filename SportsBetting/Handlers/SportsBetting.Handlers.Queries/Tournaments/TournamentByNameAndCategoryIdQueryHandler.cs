namespace SportsBetting.Handlers.Queries.Tournaments
{
    using System.Linq;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TournamentByNameAndCategoryIdQueryHandler : IQueryHandler<TournamentByNameAndCategoryIdQuery, Tournament>
    {
        private readonly ICache<Tournament> tournamentsCache;

        public TournamentByNameAndCategoryIdQueryHandler(ICache<Tournament> tournamentsCache)
        {
            this.tournamentsCache = tournamentsCache;
        }

        public Tournament Handle(TournamentByNameAndCategoryIdQuery query)
        {
            Tournament tournament = tournamentsCache
                .All(x => x.Name == query.Name && x.CategoryId == query.CategoryId)
                .FirstOrDefault();

            return tournament;
        }
    }
}