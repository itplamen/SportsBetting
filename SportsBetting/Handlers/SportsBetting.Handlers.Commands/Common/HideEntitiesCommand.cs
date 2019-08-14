namespace SportsBetting.Handlers.Commands.Common
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Commands.Contracts;

    public class HideEntitiesCommand<TEntity> : ICommand
        where TEntity : BaseModel
    {
        public HideEntitiesCommand(IEnumerable<TEntity> entities)
        {
            Entities = entities;
        }

        public IEnumerable<TEntity> Entities { get; set; }
    }
}