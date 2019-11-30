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
    public class ThreeWayOddsProviderTests
    {
        private IOddsProvider oddsProvider;
        private IList<string> validOddNames;

        [TestInitialize]
        public void TestInitialize()
        {
            oddsProvider = new ThreeWayOddsProvider(
                ServicesMock.GetHtmlService(),
                ProvidersMock.GetOddsProvider());

            validOddNames = new List<string>() { "FirstTestName", "SecondTestName", "ThirdTesName" };
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
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithValues(), new List<string>());

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenThereIsOnlyOneOddName()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithValues(), new List<string>() { "TestName" });

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeContainsOnlyOneOdd()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetMarketNodeWithOneOdd(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeContainsMoreThanThreeOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithMoreThanThreeOddsAndValues(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeContainsThreeOddsWithHeaders()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithHeaders(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnThreeOddsWithoutValuesWhenMarketNodeContainsThreeOddsWithoutValues()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithoutValues(), validOddNames);

            Assert.AreEqual(3, odds.Count());
            Assert.IsTrue(odds.All(x => x.Value == 0));
            Assert.AreEqual(0, odds.First().Rank);
            Assert.AreEqual(1, odds.Skip(1).First().Rank);
            Assert.AreEqual(2, odds.Last().Rank);
            Assert.IsFalse(odds.All(x => x.IsSuspended));
            Assert.IsTrue(odds.All(x => x.Header == 0));
            Assert.IsTrue(odds.All(x => string.IsNullOrEmpty(x.Symbol)));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Name)));
        }

        [TestMethod]
        public void GetShouldReturnOneSuspendedOddWhenMarketNodeContainsThreeOddsWithOneSuspended()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithOneSuspendedOdd(), validOddNames);

            Assert.AreEqual(3, odds.Count());
            Assert.IsTrue(odds.First().IsSuspended);
            Assert.IsTrue(odds.First().Value == 0);
            Assert.IsTrue(odds.Skip(1).First().Value > 0);
            Assert.IsTrue(odds.Last().Value > 0);
            Assert.AreEqual(0, odds.First().Rank);
            Assert.AreEqual(1, odds.Skip(1).First().Rank);
            Assert.AreEqual(2, odds.Last().Rank);
            Assert.IsFalse(odds.All(x => x.IsSuspended));
            Assert.IsTrue(odds.All(x => x.Header == 0));
            Assert.IsTrue(odds.All(x => string.IsNullOrEmpty(x.Symbol)));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Name)));
        }

        [TestMethod]
        public void GetShouldReturnOddsWithWinAndLossStatusWhenMarketNodeContainsThreeResultedOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithThreeResultedOdds(), validOddNames);

            Assert.AreEqual(3, odds.Count());
            Assert.AreEqual(1, odds.Count(x => x.ResultStatus == OddResultFeedStatus.Win));
            Assert.AreEqual(2, odds.Count(x => x.ResultStatus == OddResultFeedStatus.Loss));
            Assert.IsTrue(odds.All(x => x.Value == 0));
            Assert.AreEqual(0, odds.First().Rank);
            Assert.AreEqual(1, odds.Skip(1).First().Rank);
            Assert.AreEqual(2, odds.Last().Rank);
            Assert.IsFalse(odds.All(x => x.IsSuspended));
            Assert.IsTrue(odds.All(x => x.Header == 0));
            Assert.IsTrue(odds.All(x => string.IsNullOrEmpty(x.Symbol)));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Name)));
        }

        [TestMethod]
        public void GetShouldReturnThreeOddsWithValuesWhenMarketNodeContainsThreeValidOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithValues(), validOddNames);

            Assert.AreEqual(3, odds.Count());
            Assert.IsTrue(odds.All(x => x.Value > 0));
            Assert.AreEqual(0, odds.First().Rank);
            Assert.AreEqual(1, odds.Skip(1).First().Rank);
            Assert.AreEqual(2, odds.Last().Rank);
            Assert.IsFalse(odds.All(x => x.IsSuspended));
            Assert.IsTrue(odds.All(x => x.Header == 0));
            Assert.IsTrue(odds.All(x => string.IsNullOrEmpty(x.Symbol)));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Name)));
        }
    }
}