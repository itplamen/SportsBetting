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
            container.Register<ICommandHandler<CreateAccountCommand, Account>, CreateAccountCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<EncryptPasswordCommand, string>, EncryptPasswordCommandHandler>(new WebRequestLifestyle());
            container.Register<IValidationHandler<LoginAccountCommand>, CanLoginAccountValidationHandler>(new WebRequestLifestyle());
            container.Register<IValidationHandler<CreateAccountCommand>, CanCreateAccountValidationHandler>(new WebRequestLifestyle());
        }
    }
}