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
    public class TwoWayOddsProviderTests
    {
        private IOddsProvider oddsProvider;
        private IList<string> validOddNames;

        [TestInitialize]
        public void TestInitialize()
        {
            oddsProvider = new TwoWayOddsProvider(
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
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetTwoWayMarketNodeWithValues(), new List<string>());

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenThereIsOnlyOneOddName()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetTwoWayMarketNodeWithValues(), new List<string>() { "TestName" });

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeContainsOnlyOneOdd()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetMarketNodeWithOneOdd(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeContainsMoreThanTwoOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketContainerWithValues(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeContainsTwoOddsWithHeaders()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetTwoWayMarketNodeWithHeaders(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnTwoOddsWithoutValuesWhenMarkedtNodeContainsTwoOddsWithoutValues()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetTwoWayMarketOddWithoutValues(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.All(x => x.Value == 0));
        }

        [TestMethod]
        public void GetShouldReturnOneSuspendedOddWhenMarkedtNodeContainsTwoOddsWithOneSuspended()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetTwoWayMarketNodeWithOneSuspendedOdd(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.AreEqual(1, odds.Count(x => x.IsSuspended));
        }

        [TestMethod]
        public void GetShouldReturnOddsWithWinAndLossStatusWhenMarkedtNodeContainsTwoResultedOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetTwoWayMarketNodeWithTwoResultedOdds(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.Any(x => x.ResultStatus == OddResultFeedStatus.Win));
            Assert.IsTrue(odds.Any(x => x.ResultStatus == OddResultFeedStatus.Loss));
        }

        [TestMethod]
        public void GetShouldReturnTwoOddsWithValuesWhenMarkedtNodeContainsTwoValidOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetTwoWayMarketNodeWithValues(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.All(x => x.Value > 0));
        }
    }
}