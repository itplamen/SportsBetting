namespace SportsBetting.Feeder.Core.Tests.ProvidersTests.Teams
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Providers.Teams;
    using SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes;
    using SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs;
    using SportsBetting.Feeder.Models;

    [TestClass]
    public class TeamsProviderTests
    {
        private ITeamsProvider teamsProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            teamsProvider = new TeamsProvider();
        }

        [TestMethod]
        public void GetShouldReturnNullWhenMatchContainerIsNull()
        {
            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(null);

            Assert.AreEqual(null, teams);
        }

        [TestMethod]
        public void GetShouldReturnNullWhenMatchContainerIsInvalid()
        {
            HtmlNode matchContainer = HtmlNodesLoader.Load("<div></div>");

            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);

            Assert.AreEqual(null, teams);
        }

        [TestMethod]
        public void GetShouldReturnNullWhenMatchContainerDoesNotContainChildNodes()
        {
            HtmlNode matchContainer = TeamsProviderStub.GetInvalidMatchContainerWithMissingChildNode();

            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);

            Assert.AreEqual(null, teams);
        }

        [TestMethod]
        public void GetShouldReturnNullWhenMatchContainerDoesNotContainTeamNames()
        {
            HtmlNode matchContainer = TeamsProviderStub.GetInvalidMatchContainerWithMissingTeamNames();

            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);

            Assert.AreEqual(null, teams);
        }

        [TestMethod]
        public void GetShouldReturnNullWhenMatchContainerHasOnlyOneChildNode()
        {
            HtmlNode matchContainer = TeamsProviderStub.GetInvalidMatchContainerWithOnlyOneTeam();

            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);

            Assert.AreEqual(null, teams);
        }

        [TestMethod]
        public void GetShouldReturnTwoTeamsWithoutScoresWhenMatchContainerHasValidTeamsWithoutScores()
        {
            HtmlNode matchContainer = TeamsProviderStub.GetValidMatchContainerWithTeamsWithoutScores();

            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);

            Assert.AreEqual(2, teams.Count());
            Assert.IsTrue(teams.All(x => x.Score == 0));
        }

        [TestMethod]
        public void GetShouldReturnTwoTeamsWithScoresWhenMatchContainerHasValidTeamsWithScores()
        {
            HtmlNode matchContainer = TeamsProviderStub.GetValidMatchContainerWithTeamsAndScores();

            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);

            Assert.AreEqual(2, teams.Count());
            Assert.IsTrue(teams.All(x => x.Score > 0));
        }
    }
}