namespace SportsBetting.Handlers.Commands.Contracts
{
    public interface ICommandHandler<TCommand, TResult> 
        where TCommand : ICommand
    {
        TResult Handle(TCommand command);
    }
}