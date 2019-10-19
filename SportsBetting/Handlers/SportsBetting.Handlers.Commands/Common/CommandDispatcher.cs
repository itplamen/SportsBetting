namespace SportsBetting.Handlers.Commands.Common
{
    using System.Collections.Generic;

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
            ICommandHandler<TCommand> handler = container.GetInstance<ICommandHandler<TCommand>>();

            handler.Handle(command);
        }

        public TResult Dispatch<TCommand, TResult>(TCommand command) 
            where TCommand : ICommand
        {
            ICommandHandler<TCommand, TResult> handler = container.GetInstance<ICommandHandler<TCommand, TResult>>();

            return handler.Handle(command);
        }

        public IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) 
            where TCommand : ICommand
        {
            IValidationHandler<TCommand> handler = container.GetInstance<IValidationHandler<TCommand>>();

            return handler.Validate(command);
        }
    }
}