﻿namespace SportsBetting.Handlers.Commands.Contracts
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult> 
        where TCommand : ICommand
    {
        TResult Handle(TCommand command);
    }
}