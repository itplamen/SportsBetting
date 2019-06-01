using SportsBetting.Data.Common.Contracts;
using SportsBetting.Data.Models;
using SportsBetting.Feeder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBetting.Feeder.Core.Managers
{
    public class MatchesManager
    {
        private readonly IRepository<Match> matchesRepository;

        public MatchesManager(IRepository<Match> matchesRepository)
        {
            this.matchesRepository = matchesRepository;
        }

        public void Manage(MatchFeedModel feedModel)
        {
            Match match = matchesRepository.All(x => x.Key == feedModel.Id).FirstOrDefault();

            if (match == null)
            {

            }
        }

        private void Add()
        {

        }
    }
}
