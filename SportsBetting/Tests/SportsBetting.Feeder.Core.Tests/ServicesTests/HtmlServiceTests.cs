namespace SportsBetting.Feeder.Core.Tests.ServicesTests
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Core.Services;
    using SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes;
    using SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs;
    using SportsBetting.Feeder.Models;

    [TestClass]
    public class HtmlServiceTests
    {
        private IHtmlService htmlService;

        [TestInitialize]
        public void TestInitialize()
        {
            htmlService = new HtmlService();
        }

        [TestMethod]
        public void GetOddNamesShouldReturnNullWhenMarketNodeIsNull()
        {
            IEnumerable<string> oddNames = htmlService.GetOddNames(null);

            Assert.IsTrue(oddNames == null);
        }

        [TestMethod]
        public void GetOddNamesShouldReturnNullWhenMarketNodeHasInvalidClass()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div></div>");

            IEnumerable<string> oddNames = htmlService.GetOddNames(marketNode);

            Assert.IsTrue(oddNames == null);
        }

        [TestMethod]
        public void GetOddNamesShouldReturnNullWhenMarketNodeDOesNotHaveParentNode()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div class='tableColumnNames__column___-zFNK'></div>");

            IEnumerable<string> oddNames = htmlService.GetOddNames(marketNode);

            Assert.IsTrue(oddNames == null);
        }

        [TestMethod]
        public void GetOddNamesShouldReturnOneOddNameWhenMarketNodeHasParentNodeAndValidClass()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div><div class='tableColumnNames__column___-zFNK'></div></div>");

            IList<string> oddNames = htmlService.GetOddNames(marketNode);

            Assert.AreEqual(1, oddNames.Count);
        }

        [TestMethod]
        public void GetMatchUrlsShouldNotReturnOddNamesWhenPageSourceIsEmpty()
        {
            IEnumerable<string> oddNames = htmlService.GetMatchUrls(MatchXPaths.EVENT_BODY, string.Empty);

            Assert.IsFalse(oddNames.Any());
        }

        [TestMethod]
        public void GetMatchUrlsShouldNotReturnOddNamesWhenPageSourceIsInvalid()
        {
            IEnumerable<string> oddNames = htmlService.GetMatchUrls(MatchXPaths.EVENT_BODY, "<div></div>");

            Assert.IsFalse(oddNames.Any());
        }

        [TestMethod]
        public void GetMatchUrlsShouldReturnOneOddNameWhenXPathAndPageSourceAreValid()
        {
            IEnumerable<string> oddNames = htmlService.GetMatchUrls(MatchXPaths.EVENT_BODY, HtmlServiceStub.GetEventWithOneUrl());

            Assert.AreEqual(1, oddNames.Count());
        }

        [TestMethod]
        public void GetMatchContainerShouldReturnNullWhenPageSourceIsEmpty()
        {
            HtmlNode matchContainer = htmlService.GetMatchContainer(ContainerXPaths.MATCH, string.Empty);

            Assert.IsTrue(matchContainer == null);
        }

        [TestMethod]
        public void GetMatchContainerShouldReturnNullWhenPageSourceIsInvalid()
        {
            HtmlNode matchContainer = htmlService.GetMatchContainer(ContainerXPaths.MATCH, "<div></div>");

            Assert.IsTrue(matchContainer == null);
        }

        [TestMethod]
        public void GetMatchContainerShouldReturnHtmlNodeWHenXPathAndPageSourceAreValid()
        {
            HtmlNode matchContainer = htmlService.GetMatchContainer(ContainerXPaths.MATCH, "<div class='Match__container'></div>");

            Assert.IsTrue(matchContainer != null);
        }

        [TestMethod]
        public void GetTwoWayOddsCountShouldReturnZeroCountWhenMarketNodeIsNull()
        {
            int count = htmlService.GetTwoWayOddsCount(null);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetTwoWayOddsCountShouldReturnZeroCountWhenMarketNodeIsInvalid()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div></div>");
            int count = htmlService.GetTwoWayOddsCount(marketNode);

            Assert.AreEqual(0, count);
        }
        
        [TestMethod]
        public void GetTwoWayOdddsCountShouldReturnZeroCountWhenMarketNodeIsInvalid()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div></div>");
            int count = htmlService.GetTwoWayOddsCount(marketNode);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetTwoWayOdddsCountShouldReturnZeroCountWhenMarketNodeDoesNotHaveChildNodes()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div><div class='tableMarketRow__container___3jeni'></div></div>");
            int count = htmlService.GetTwoWayOddsCount(marketNode);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetTwoWayOdddsCountShouldReturnZeroCountWhenMarketNodeHasOneEmptyChildNode()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div><div class='tableMarketRow__container___3jeni'><div></div<</div></div>");
            int count = htmlService.GetTwoWayOddsCount(marketNode);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetTwoWayOdddsCountShouldReturnOneCountWhenMarketNodeHasOneChildNode()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div><div class='tableMarketRow__container___3jeni'><div class='tableMarketRow__odd-wrapper___3nnKr'></div<</div></div>");
            int count = htmlService.GetTwoWayOddsCount(marketNode);

            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void GetOddsCountShouldReturnZeroCountWhenMarketNodeIsNull()
        {
            int count = htmlService.GetOddsCount(null);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetOddsCountShouldReturnZeroCountWhenMarketNodeIsInvalid()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div></div>");

            int count = htmlService.GetOddsCount(marketNode);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetOddsCountShouldReturnZeroCountWhenMarketNodeHasNoChildNodes()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div><div class='tableMarketRow__container___3jeni'></div></div>");

            int count = htmlService.GetOddsCount(marketNode);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetOddsCountShouldReturnOneCountWhenMarketNodeContainsOneChildNode()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div><div class='tableMarketRow__container___3jeni'><div></div></div></div>");

            int count = htmlService.GetOddsCount(marketNode);

            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void GetOddResultStatusShouldReturnNotResultedWhenOddNodeIsNull()
        {
            OddResultFeedStatus status = htmlService.GetOddResultStatus(null);

            Assert.AreEqual(OddResultFeedStatus.NotResulted, status);
        }

        [TestMethod]
        public void GetOddResultStatusShouldReturnNotResultedWhenOddNodeIsInvalid()
        {
            HtmlNode oddNode = HtmlNodesLoader.Load("<div></div>");

            OddResultFeedStatus status = htmlService.GetOddResultStatus(oddNode);

            Assert.AreEqual(OddResultFeedStatus.NotResulted, status);
        }

        [TestMethod]
        public void GetOddResultStatusShouldReturnNotResultedWhenOddNodeDoesNotContainInnerText()
        {
            HtmlNode oddNode = HtmlNodesLoader.Load("<div><div class='odd__result___1K5vV'></div></div>");

            OddResultFeedStatus status = htmlService.GetOddResultStatus(oddNode);

            Assert.AreEqual(OddResultFeedStatus.NotResulted, status);
        }

        [TestMethod]
        public void GetOddResultStatusShouldReturnLossWhenOddNodeContainsInnerTextLossInUpperCase()
        {
            HtmlNode oddNode = HtmlNodesLoader.Load("<div><div class='odd__result___1K5vV'>LOSS</div></div>");

            OddResultFeedStatus status = htmlService.GetOddResultStatus(oddNode);

            Assert.AreEqual(OddResultFeedStatus.Loss, status);
        }

        [TestMethod]
        public void GetOddResultStatusShouldReturnWinWhenOddNodeContainsInnerTextWinInLowerCase()
        {
            HtmlNode oddNode = HtmlNodesLoader.Load("<div><div class='odd__result___1K5vV'>win</div></div>");

            OddResultFeedStatus status = htmlService.GetOddResultStatus(oddNode);

            Assert.AreEqual(OddResultFeedStatus.Win, status);
        }

        [TestMethod]
        public void HasHeaderShouldReturnFalseWhenMarketNodeIsNull()
        {
            bool hasHeader = htmlService.HasHeader(null);

            Assert.IsFalse(hasHeader);
        }
        
        [TestMethod]
        public void HasHeaderShouldReturnFalseWhenMarketNodeIsInvalid()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div></div>");

            bool hasHeader = htmlService.HasHeader(marketNode);

            Assert.IsFalse(hasHeader);
        }

        [TestMethod]
        public void HasHeaderShouldReturnTrueWhenMarketNodeHasValidClass()
        {
            HtmlNode marketNode = HtmlNodesLoader.Load("<div><div class='tableMarketRow__specifier-value___3S715'></div></div>");

            bool hasHeader = htmlService.HasHeader(marketNode);

            Assert.IsTrue(hasHeader);
        }

        [TestMethod]
        public void IsSuspendedShouldReturnFalseWhenOddNodeIsNull()
        {
            bool isSuspended = htmlService.IsSuspended(null);

            Assert.IsFalse(isSuspended);
        }

        [TestMethod]
        public void IsSuspendedShouldReturnFalseWhenOddNodeIsInvalid()
        {
            HtmlNode oddNode = HtmlNodesLoader.Load("<div></div>");

            bool isSuspended = htmlService.IsSuspended(oddNode);

            Assert.IsFalse(isSuspended);
        }

        [TestMethod]
        public void IsSuspendedShouldReturnFalseWhenOddNodeContainsValidTitleButDoesNotContainValidClass()
        {
            HtmlNode oddNode = HtmlNodesLoader.Load("<div title='Deactivated'></div>");

            bool isSuspended = htmlService.IsSuspended(oddNode);

            Assert.IsFalse(isSuspended);
        }

        [TestMethod]
        public void IsSuspendedShouldReturnFalseWhenOddNodeDoesNotContainValidTitleButContainsValidClass()
        {
            HtmlNode oddNode = HtmlNodesLoader.Load("<div title='InvalidTitle' class='__app-Odd-is-disabled'></div>");

            bool isSuspended = htmlService.IsSuspended(oddNode);

            Assert.IsFalse(isSuspended);
        }

        [TestMethod]
        public void IsSuspendedShouldReturnTrueWhenOddNodeContainsValidTitleAndClass()
        {
            HtmlNode oddNode = HtmlNodesLoader.Load("<div title='Deactivated' class='__app-Odd-is-disabled'></div>");

            bool isSuspended = htmlService.IsSuspended(oddNode);

            Assert.IsTrue(isSuspended);
        }
    }
}