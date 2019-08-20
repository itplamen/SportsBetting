namespace SportsBetting.Feeder.Core.Providers.Tournaments
{
    using System.Linq;
    using System.Net;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Factories;
    using SportsBetting.Feeder.Models;

    public class TournametsProvider : ITournametsProvider
    {
        public TournamentFeedModel Get(HtmlNode matchInfo)
        {
            string name = WebUtility.HtmlDecode(matchInfo.FirstChild.FirstChild.InnerText);
            string category = matchInfo.ChildNodes[1].InnerText.Split(',').FirstOrDefault();
            TournamentFeedModel tournament = ObjectFactory.CreateTournament(name, category);

            return tournament;
        }
    }
}