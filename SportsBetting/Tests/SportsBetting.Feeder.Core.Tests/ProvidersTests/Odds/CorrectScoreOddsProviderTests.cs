namespace SportsBetting.Feeder.Core.Tests.ProvidersTests.Odds
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Providers.Odds;
    using SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Mocks;
    using SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs;
    using SportsBetting.Feeder.Models;

    [TestClass]
    public class CorrectScoreOddsProviderTests
    {
        private IOddsProvider oddsProvider;
        private IList<string> validOddNames;

        [TestInitialize]
        public void TestInitialize()
        {
            oddsProvider = new CorrectScoreOddsProvider(
                ServicesMock.GetHtmlService(),
                ProvidersMock.GetOddsProvider());

            validOddNames = new List<string>() { "FirstTestName", "SecondTestName" };
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeIsNull()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(null, validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenOddNamesAreMissing()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetCorrectScoreMarketNodeWithValues(), new List<string>());

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenThereIsOnlyOneOddName()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetCorrectScoreMarketNodeWithValues(), new List<string>() { "TestName" });

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeDoesNotContainMarketName()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetCorrectScoreMarketNodeWithoutMarketName(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnTwoOddsWithoutValuesWhenMarketNodeContainsTwoOddsWithoutValues()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetCorrectScoreMarketNodeWithoutValues(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.All(x => x.Value == 0));
        }

        [TestMethod]
        public void GetShouldReturnOneSuspendedOddWhenMarketNodeContainsTwoOddsWithOneSuspended()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetCorrectScoreMarketNodeWithOneSuspendedOdd(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.AreEqual(1, odds.Count(x => x.IsSuspended));
        }

        [TestMethod]
        public void GetShouldReturnOddsWithWinAndLossStatusWhenMarketNodeContainsTwoResultedOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetCorrectScoreMarketNodeWithOneResultedOdd(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.Any(x => x.ResultStatus == OddResultFeedStatus.Win));
            Assert.IsTrue(odds.Any(x => x.ResultStatus == OddResultFeedStatus.Loss));
        }

        [TestMethod]
        public void GetShouldReturnTwoOddsWithValuesWhenMarketNodeContainsTwoValidOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetCorrectScoreMarketNodeWithValues(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.All(x => x.Value > 0));
        }
    }
}