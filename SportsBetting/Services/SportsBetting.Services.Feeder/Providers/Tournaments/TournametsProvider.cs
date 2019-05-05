namespace SportsBetting.Services.Feeder.Providers.Tournaments
{
    using System.Linq;
    using System.Net;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Providers;

    public class TournametsProvider : ITournametsProvider
    {
        private readonly IObjectFactory objectFactory;

        public TournametsProvider(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public Tournament Get(HtmlNode matchInfo)
        {
            string name = WebUtility.HtmlDecode(matchInfo.FirstChild.FirstChild.InnerText);
            string category = matchInfo.ChildNodes[1].InnerText.Split(',').FirstOrDefault();
            Tournament tournament = objectFactory.CreateTournament(name, category);

            return tournament;
        }
    }
}