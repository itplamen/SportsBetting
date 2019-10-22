namespace SportsBetting.Handlers.Queries.Tournaments
{
    using System.Linq;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TournamentByNameQueryHandler : IQueryHandler<TournamentByNameQuery, Tournament>
    {
        private readonly ICache<Tournament> tournamentsCache;

        public TournamentByNameQueryHandler(ICache<Tournament> tournamentsCache)
        {
            this.tournamentsCache = tournamentsCache;
        }

        public Tournament Handle(TournamentByNameQuery query)
        {
            Tournament tournament = tournamentsCache
                .All(x => x.Name == query.Name)
                .FirstOrDefault();

            return tournament;
        }
    }
}