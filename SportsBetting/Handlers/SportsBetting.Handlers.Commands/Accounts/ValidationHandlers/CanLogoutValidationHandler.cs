namespace SportsBetting.Handlers.Commands.Accounts.ValidationHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Common;
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
            IEnumerable<string> ids = new List<string>() { command.LoginToken };
            EntitiesByIdQuery<Authentication> query = new EntitiesByIdQuery<Authentication>(ids);

            IEnumerable<Authentication> authentications = queryHandler.Handle(query);

            if (authentications == null || !authentications.Any())
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.LoginToken), "Could not find login record!")
                };
            }

            if (authentications.First().Expiration < DateTime.UtcNow)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.LoginToken), "Login expired!")
                };
            }

            return Enumerable.Empty<ValidationResult>();
        }
    }
}