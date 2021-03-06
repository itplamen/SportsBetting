﻿namespace SportsBetting.IoCContainer.Packages.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.CommandHandlers;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Auth.CommandHandlers;
    using SportsBetting.Handlers.Commands.Auth.Commands;
    using SportsBetting.Handlers.Commands.Auth.ValidationHandlers;
    using SportsBetting.Handlers.Commands.Bets.CommandHandlers;
    using SportsBetting.Handlers.Commands.Bets.Commands;
    using SportsBetting.Handlers.Commands.Bets.ValidationHandlers;
    using SportsBetting.Handlers.Commands.Common;
    using SportsBetting.Handlers.Commands.Common.CommandHandlers;
    using SportsBetting.Handlers.Commands.Common.Commands;
    using SportsBetting.Handlers.Commands.Common.ValidationHandlers;
    using SportsBetting.Handlers.Commands.Contracts;

    public sealed class CommandHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ICommandDispatcher, CommandDispatcher>(new WebRequestLifestyle());
            container.Register<ICommandHandler<AccountCommand, Account>, RegisterCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<LogoutCommand>, LogoutCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<PasswordCommand, string>, EncryptPasswordCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<AuthCommand, Authentication>, LoginCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<UpdateAccountCommand>, UpdateAccountCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<PlaceBetCommand, string>, PlaceBetCommandHandler>(new WebRequestLifestyle());

            container.Register<IValidationHandler<AccountCommand>, CanLoginValidationHandler>(new WebRequestLifestyle());
            container.Register<IValidationHandler<LogoutCommand>, CanLogoutValidationHandler>(new WebRequestLifestyle());
            container.Register<IValidationHandler<PlaceBetCommand>, CanPlaceBetValidationCommand>(new WebRequestLifestyle());
            container.Register<IValidationHandler<UsernameCommand>, UniqueUsernameValidationHandler>(new WebRequestLifestyle());
        }
    }
}