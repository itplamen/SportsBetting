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
        private readonly IQueryHandler<TournamentByNameAndCategoryIdQuery, Tournament> tournamentByNameAndCategoryIdHandler;
        private readonly ICommandHandler<CreateTournamentCommand, string> createTournamentHandler;

        public TournamentsManager(
            IQueryHandler<TournamentByNameAndCategoryIdQuery, Tournament> tournamentByNameAndCategoryIdHandler,
            ICommandHandler<CreateTournamentCommand, string> createTournamentHandler)
        {
            this.tournamentByNameAndCategoryIdHandler = tournamentByNameAndCategoryIdHandler;
            this.createTournamentHandler = createTournamentHandler;
        }

        public string Manage(TournamentFeedModel feedModel, string categoryId)
        {
            TournamentByNameAndCategoryIdQuery query = new TournamentByNameAndCategoryIdQuery(feedModel.Name, categoryId);
            Tournament tournament = tournamentByNameAndCategoryIdHandler.Handle(query);

            if (tournament != null)
            {
                return tournament.Id;
            }

            CreateTournamentCommand command = Mapper.Map<CreateTournamentCommand>(feedModel);
            command.CategoryId = categoryId;

            return createTournamentHandler.Handle(command);
        }       
    }
}