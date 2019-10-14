namespace SportsBetting.Handlers.Commands.Contracts
{
    using SportsBetting.Common.Results;

    public interface IValidationHandler<in TCommand> 
        where TCommand : ICommand
    {
        ValidationResult Validate(TCommand command);
    }
}