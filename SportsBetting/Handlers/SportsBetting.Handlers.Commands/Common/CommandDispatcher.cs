namespace SportsBetting.Handlers.Commands.Common
{
    using SimpleInjector;

    using SportsBetting.Common.Results;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Container container;

        public CommandDispatcher(Container container)
        {
            this.container = container;
        }

        public void Dispatch<TCommand>(TCommand command) 
            where TCommand : ICommand
        {
            ICommandHandler<ICommand> handler = container.GetInstance<ICommandHandler<ICommand>>();

            handler.Handle(command);
        }

        public TResult Dispatch<TCommand, TResult>(TCommand command) 
            where TCommand : ICommand
        {
            ICommandHandler<ICommand, TResult> handler = container.GetInstance<ICommandHandler<ICommand, TResult>>();

            return handler.Handle(command);
        }

        public ValidationResult Validate<TCommand>(TCommand command) 
            where TCommand : ICommand
        {
            IValidationHandler<ICommand> handler = container.GetInstance<IValidationHandler<ICommand>>();

            return handler.Validate(command);
        }
    }
}