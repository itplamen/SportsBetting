namespace SportsBetting.Feeder.Core.Managers
{
    using AutoMapper;

    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Tournaments;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Tournaments;

    public class TournamentsManager : ITournamentsManager
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public TournamentsManager(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        public string Manage(TournamentFeedModel feedModel)
        {
            TournamentByNameQuery query = new TournamentByNameQuery(feedModel.Name);
            Tournament tournament = queryDispatcher.Dispatch<TournamentByNameQuery, Tournament>(query);

            if (tournament != null)
            {
                return tournament.Id;
            }

            CreateTournamentCommand command = Mapper.Map<CreateTournamentCommand>(feedModel);
            string tournamentId = commandDispatcher.Dispatch<CreateTournamentCommand, string>(command);

            return tournamentId;
        }       
    }
}