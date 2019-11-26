namespace SportsBetting.Feeder.Core.Tests.ProvidersTests.Tournaments
{
    using HtmlAgilityPack;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Providers.Tournaments;
    using SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes;
    using SportsBetting.Feeder.Models;

    [TestClass]
    public class TournametsProviderTests
    {
        private ITournamentsProvider tournamentsProvider; 

        [TestInitialize]
        public void TestInitialize()
        {
            tournamentsProvider = new TournamentsProvider();
        }

        [TestMethod]
        public void GetShouldReturnTournamentWithoutNameWhenMatchInfoIsNull()
        {
            TournamentFeedModel tournament = tournamentsProvider.Get(null);

            Assert.IsTrue(string.IsNullOrEmpty(tournament.Name));
        }

        [TestMethod]
        public void GetShouldReturnTournamentWithoutNameWhenMatchInfoIsInvalid()
        {
            HtmlNode matchInfo = HtmlNodesLoader.Load("<div></div>");

            TournamentFeedModel tournament = tournamentsProvider.Get(matchInfo);

            Assert.IsTrue(string.IsNullOrEmpty(tournament.Name));
        }

        [TestMethod]
        public void GetShouldReturnTournamentWithoutNameWhenMatchInfoDoesNotHaveInnerText()
        {
            HtmlNode matchInfo = HtmlNodesLoader.Load("<div><div class='__app-PromoMatchBody-tournament promoMatchBody__tournament___1M0Aj'></div></div>");

            TournamentFeedModel tournament = tournamentsProvider.Get(matchInfo);

            Assert.IsTrue(string.IsNullOrEmpty(tournament.Name));
        }

        [TestMethod]
        public void GetShouldReturnTournamentWithNameWhenMatchInfoContainsInnerText()
        {
            HtmlNode matchInfo = HtmlNodesLoader.Load("<div><div class='__app-PromoMatchBody-tournament promoMatchBody__tournament___1M0Aj'>TournamentName</div></div>");

            TournamentFeedModel tournament = tournamentsProvider.Get(matchInfo);

            Assert.IsFalse(string.IsNullOrEmpty(tournament.Name));
        }
    }
}