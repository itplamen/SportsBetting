namespace SportsBetting.Services.Feeder.Providers.Tournaments
{
    using System.Linq;
    using System.Net;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Providers;
    using SportsBetting.Services.Feeder.Factories;

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