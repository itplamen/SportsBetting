namespace SportsBetting.Services.Data
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class OddsService : IOddsService
    {
        private readonly IRepository<Odd> oddsRepository;

        public OddsService(IRepository<Odd> oddsRepository)
        {
            this.oddsRepository = oddsRepository;
        }

        public string Add(Odd odd, string marketId, string matchId)
        {
            odd.MarketId = marketId;
            odd.MatchId = matchId;

            oddsRepository.Add(odd);

            return odd.Id;
        }

        public Odd Get(int key)
        {
            Odd odd = oddsRepository.All(x => x.Key == key).FirstOrDefault();

            return odd;
        }

        public Odd Update(string id, Odd odd)
        {
            Odd oddToUpdate = oddsRepository.All(x => x.Id == id).FirstOrDefault();

            if (oddToUpdate != null)
            {
                oddToUpdate.Value = odd.Value;
                oddToUpdate.IsActive = true;
                oddToUpdate.IsSuspended = odd.IsSuspended;
                oddToUpdate.ResultStatus = odd.ResultStatus;

                oddsRepository.Update(oddToUpdate);
            }

            return oddToUpdate;
        }
    }
}