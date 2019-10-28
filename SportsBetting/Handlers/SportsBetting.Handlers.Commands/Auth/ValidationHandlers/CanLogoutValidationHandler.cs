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
        private readonly IQueryHandler<EntityByIdQuery<Authentication>, Authentication> queryHandler;

        public CanLogoutValidationHandler(IQueryHandler<EntityByIdQuery<Authentication>, Authentication> queryHandler)
        {
            this.queryHandler = queryHandler;
        }

        public IEnumerable<ValidationResult> Validate(LogoutCommand command)
        {
            Authentication authentications = queryHandler.Handle(new EntityByIdQuery<Authentication>(command.Id));

            if (authentications == null)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.Id), "Could not find login record!")
                };
            }

            if (authentications.Expiration < DateTime.UtcNow)
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