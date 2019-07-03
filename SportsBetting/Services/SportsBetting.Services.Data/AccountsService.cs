namespace SportsBetting.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class AccountsService : IAccountsService
    {
        private readonly IRepository<Account> accountsRepository;

        public AccountsService(IRepository<Account> accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }

        public string Add(Account account)
        {
            accountsRepository.Add(account);

            return account.Id;
        }

        public Account GetByUsername(string username)
        {
            Account account = accountsRepository.All(x => x.Username == username).FirstOrDefault();

            return account;
        }

        public Account GetByEmail(string email)
        {
            Account account = accountsRepository.All(x => x.Email == email).FirstOrDefault();

            return account;
        }

        public IEnumerable<Account> AllWithDeleted()
        {
            IEnumerable<Account> accounts = accountsRepository.All(x => true);

            return accounts;
        }
    }
}