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

        public string Add(int key, string name, string matchId)
        {
            Market market = new Market()
            {
                Key = key,
                Name = name,
                MatchId = matchId
            };

            marketsRepository.Add(market);

            return market.Id;
        }

        public Market Get(int key)
        {
            Market market = marketsRepository.All(x => x.Key == key).FirstOrDefault();

            return market;
        }
    }
}