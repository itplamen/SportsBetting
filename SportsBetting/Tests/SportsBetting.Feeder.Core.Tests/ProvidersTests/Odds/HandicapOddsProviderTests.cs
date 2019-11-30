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
    public class HandicapOddsProviderTests
    {
        private IOddsProvider oddsProvider;
        private IList<string> validOddNames;

        [TestInitialize]
        public void TestInitialize()
        {
            oddsProvider = new HandicapOddsProvider(ServicesMock.GetHtmlService());
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
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetHandicapMarketNodeWithValues(), new List<string>());

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenThereIsOnlyOneOddName()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetHandicapMarketNodeWithValues(), new List<string>() { "TestName" });

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeDoesNotContainMarketName()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetHandicapMarketNodeWithMissingMarketName(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeContainsOnlyOneOdd()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetMarketNodeWithOneOdd(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnEmptyCollectionWhenMarketNodeContainsMoreOddsThanNeeded()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetThreeWayMarketNodeWithMoreThanThreeOddsAndValues(), validOddNames);

            Assert.IsFalse(odds.Any());
        }

        [TestMethod]
        public void GetShouldReturnTwoOddsWithoutValuesWhenMarketNodeContainsTwoOddsWithoutValues()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetHandicapMarketNodeWithoutValues(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.All(x => x.Value == 0));
            Assert.IsTrue(odds.All(x => x.Header > 0));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Symbol)));
            Assert.AreEqual(0, odds.First().Rank);
            Assert.AreEqual(1, odds.Last().Rank);
            Assert.IsFalse(odds.All(x => x.IsSuspended));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Name)));
        }

        [TestMethod]
        public void GetShouldReturnOneSuspendedOddWhenMarketNodeContainsTwoOddsWithOneSuspended()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetHandicapMarketNodeWithSuspendedOdd(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.First().IsSuspended);
            Assert.IsTrue(odds.First().Value == 0);
            Assert.IsFalse(odds.Last().IsSuspended);
            Assert.IsTrue(odds.Last().Value > 0);
            Assert.IsTrue(odds.All(x => x.Header > 0));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Symbol)));
            Assert.AreEqual(0, odds.First().Rank);
            Assert.AreEqual(1, odds.Last().Rank);
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Name)));
        }

        [TestMethod]
        public void GetShouldReturnOddsWithWinAndLossStatusWhenMarketNodeContainsTwoResultedOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetHandicapMarketNodeWithResultedOdd(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.Any(x => x.ResultStatus == OddResultFeedStatus.Win));
            Assert.IsTrue(odds.Any(x => x.ResultStatus == OddResultFeedStatus.Loss));
            Assert.IsTrue(odds.All(x => x.Header > 0));
            Assert.IsTrue(odds.All(x => x.Value == 0));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Symbol)));
            Assert.AreEqual(0, odds.First().Rank);
            Assert.AreEqual(1, odds.Last().Rank);
            Assert.IsFalse(odds.All(x => x.IsSuspended));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Name)));
        }

        [TestMethod]
        public void GetShouldReturnTwoOddsWithValuesWhenMarketNodeContainsTwoValidOdds()
        {
            IEnumerable<OddFeedModel> odds = oddsProvider.Get(OddsProviderStub.GetHandicapMarketNodeWithValues(), validOddNames);

            Assert.AreEqual(2, odds.Count());
            Assert.IsTrue(odds.All(x => x.Value > 0));
            Assert.IsTrue(odds.All(x => x.Header > 0));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Symbol)));
            Assert.AreEqual(0, odds.First().Rank);
            Assert.AreEqual(1, odds.Last().Rank);
            Assert.IsFalse(odds.All(x => x.IsSuspended));
            Assert.IsFalse(odds.All(x => string.IsNullOrEmpty(x.Name)));
        }
    }
}