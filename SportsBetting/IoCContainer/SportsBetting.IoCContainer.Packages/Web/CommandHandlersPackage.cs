namespace SportsBetting.IoCContainer.Packages.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.CommandHandlers;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Accounts.ValidationHandlers;
    using SportsBetting.Handlers.Commands.Common;
    using SportsBetting.Handlers.Commands.Contracts;

    public sealed class CommandHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ICommandDispatcher, CommandDispatcher>(new WebRequestLifestyle());
            container.Register<ICommandHandler<RegisterCommand, Account>, RegisterCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<PasswordCommand, string>, EncryptPasswordCommandHandler>(new WebRequestLifestyle());
            container.Register<IValidationHandler<AccountCommand>, CanLoginValidationHandler>(new WebRequestLifestyle());
            container.Register<IValidationHandler<RegisterCommand>, CanRegisterValidationHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<LoginCommand, Authentication>, AuthenticateAccountCommandHandler>(new WebRequestLifestyle());
        }
    }
}