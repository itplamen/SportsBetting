namespace SportsBetting.Handlers.Commands.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Common.Results;

    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand;

        TResult Dispatch<TCommand, TResult>(TCommand command)
            where TCommand : ICommand;

        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}