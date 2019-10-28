namespace SportsBetting.Handlers.Commands.Auth.ValidationHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Auth.Commands;
    using SportsBetting.Handlers.Commands.Contracts;    
    using SportsBetting.Handlers.Queries.Common.Queries;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CanLogoutValidationHandler : IValidationHandler<LogoutCommand>
    {
        private readonly IQueryHandler<EntitiesByIdQuery<Authentication>, IEnumerable<Authentication>> queryHandler;

        public CanLogoutValidationHandler(IQueryHandler<EntitiesByIdQuery<Authentication>, IEnumerable<Authentication>> queryHandler)
        {
            this.queryHandler = queryHandler;
        }

        public IEnumerable<ValidationResult> Validate(LogoutCommand command)
        {
            IEnumerable<string> ids = new List<string>() { command.Id };
            EntitiesByIdQuery<Authentication> query = new EntitiesByIdQuery<Authentication>(ids);

            IEnumerable<Authentication> authentications = queryHandler.Handle(query);

            if (authentications == null || !authentications.Any())
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.Id), "Could not find login record!")
                };
            }

            if (authentications.First().Expiration < DateTime.UtcNow)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.Id), "Login expired!")
                };
            }

            return Enumerable.Empty<ValidationResult>();
        }
    }
}