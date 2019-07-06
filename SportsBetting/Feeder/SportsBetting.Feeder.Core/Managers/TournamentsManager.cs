namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Tournaments;
    using SportsBetting.Services.Data.Contracts;

    public class TournamentsManager : ITournamentsManager
    {
        private readonly ITournamentsService tournamentsService;
        private readonly ICommandHandler<CreateTournamentCommand, string> createTournamentHandler;

        public TournamentsManager(ITournamentsService tournamentsService, ICommandHandler<CreateTournamentCommand, string> createTournamentHandler)
        {
            this.tournamentsService = tournamentsService;
            this.createTournamentHandler = createTournamentHandler;
        }

        public string Manage(TournamentFeedModel feedModel, string categoryId)
        {
            Tournament tournament = tournamentsService.Get(feedModel.Name, categoryId);

            if (tournament != null)
            {
                return tournament.Id;
            }

            CreateTournamentCommand command = new CreateTournamentCommand()
            {
                Key = feedModel.Id,
                Name = feedModel.Name,
                CategoryId = categoryId
            };

            return createTournamentHandler.Handle(command);
        }       
    }
}