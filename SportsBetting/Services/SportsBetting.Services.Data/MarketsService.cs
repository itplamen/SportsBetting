namespace SportsBetting.Services.Data
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class MarketsService : IMarketsService
    {
        private readonly IRepository<Market> marketsRepository;

        public MarketsService(IRepository<Market> marketsRepository)
        {
            this.marketsRepository = marketsRepository;
        }

        public Market Get(int key)
        {
            Market market = marketsRepository.All(x => x.Key == key).FirstOrDefault();

            return market;
        }
    }
}