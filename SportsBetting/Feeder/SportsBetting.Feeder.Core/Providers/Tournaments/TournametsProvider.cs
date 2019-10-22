namespace SportsBetting.Feeder.Core.Providers.Tournaments
{
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
            TournamentFeedModel tournament = ObjectFactory.CreateTournament(name);

            return tournament;
        }
    }
}