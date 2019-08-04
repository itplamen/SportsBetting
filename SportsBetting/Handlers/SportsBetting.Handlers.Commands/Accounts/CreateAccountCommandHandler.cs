namespace SportsBetting.Handlers.Commands.Accounts
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, string>
    {
        private readonly ISportsBettingDbContext dbContext;

        public CreateAccountCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Handle(CreateAccountCommand command)
        {
            Account account = Mapper.Map<Account>(command);
            account.Password = EncryptPassword(command.Password);
            account.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Account>().InsertOne(account);

            return account.Id;
        }

        private string EncryptPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha512.ComputeHash(bytes);

                return GetStringFromHash(hash);
            }
        }

        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}