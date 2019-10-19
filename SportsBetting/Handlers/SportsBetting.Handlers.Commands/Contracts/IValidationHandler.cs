namespace SportsBetting.Handlers.Commands.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Common.Results;

    public interface IValidationHandler<in TCommand> 
        where TCommand : ICommand
    {
        IEnumerable<ValidationResult> Validate(TCommand command);
    }
}