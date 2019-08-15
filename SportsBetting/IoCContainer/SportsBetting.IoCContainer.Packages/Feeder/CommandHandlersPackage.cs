namespace SportsBetting.IoCContainer.Packages.Feeder
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Handlers.Commands.Categories;
    using SportsBetting.Handlers.Commands.Common;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Markets;
    using SportsBetting.Handlers.Commands.Matches;
    using SportsBetting.Handlers.Commands.Odds;
    using SportsBetting.Handlers.Commands.Teams;
    using SportsBetting.Handlers.Commands.Tournaments;

    public sealed class CommandHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(typeof(ICommandHandler<>), typeof(DeleteEntitiesCommandHandler<>), Lifestyle.Singleton);
            container.Register<ICommandHandler<CreateOddCommand, string>, CreateOddCommandHandler>(Lifestyle.Singleton);
            container.Register<ICommandHandler<CreateTeamCommand, string>, CreateTeamCommandHandler>(Lifestyle.Singleton);
            container.Register<ICommandHandler<CreateMatchCommand, string>, CreateMatchCommandHandler>(Lifestyle.Singleton);
            container.Register<ICommandHandler<CreateMarketCommand, string>, CreateMarketCommandHandler>(Lifestyle.Singleton);
            container.Register<ICommandHandler<CreateCategoryCommand, string>, CreateCategoryCommandHandler>(Lifestyle.Singleton);
            container.Register<ICommandHandler<CreateTournamentCommand, string>, CreateTournamentCommandHandler>(Lifestyle.Singleton);
            container.Register<ICommandHandler<UpdateMatchCommand, string>, UpdateMatchCommandHandler>(Lifestyle.Singleton);
            container.Register<ICommandHandler<UpdateOddCommand, string>, UpdateOddCommandHandler>(Lifestyle.Singleton);
        }
    }
}