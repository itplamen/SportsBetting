namespace SportsBetting.Handlers.Commands.Contracts
{
    using SportsBetting.Common.Results;

    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand;

        TResult Dispatch<TCommand, TResult>(TCommand command)
            where TCommand : ICommand;

        ValidationResult Validate<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}