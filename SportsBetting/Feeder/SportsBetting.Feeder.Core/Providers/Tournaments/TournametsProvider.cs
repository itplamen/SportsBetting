namespace SportsBetting.Feeder.Core.Providers.Tournaments
{
    using System.Net;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Factories;
    using SportsBetting.Feeder.Models;

    public class TournametsProvider : ITournametsProvider
    {
        public TournamentFeedModel Get(HtmlNode matchInfo)
        {
            HtmlNode node = matchInfo.SelectSingleNode(TournamentXPaths.NODE);
            string name = WebUtility.HtmlDecode(node.InnerText);

            TournamentFeedModel tournament = ObjectFactory.CreateTournament(name);

            return tournament;
        }
    }
}