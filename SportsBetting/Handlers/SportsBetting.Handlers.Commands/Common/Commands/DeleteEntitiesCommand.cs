﻿namespace SportsBetting.Handlers.Commands.Common.Commands
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Commands.Contracts;

    public class DeleteEntitiesCommand<TEntity> : ICommand
        where TEntity : BaseModel
    {
        public DeleteEntitiesCommand(IEnumerable<TEntity> entities)
        {
            Entities = entities;
        }

        public IEnumerable<TEntity> Entities { get; set; }
    }
}