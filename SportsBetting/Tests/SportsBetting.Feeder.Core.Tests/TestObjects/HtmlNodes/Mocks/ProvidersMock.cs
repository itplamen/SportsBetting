namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Mocks
{
    using Moq;

    using SportsBetting.Feeder.Core.Contracts.Providers;

    public static class ProvidersMock
    {
        public static IOddsProvider GetOddsProvider()
        {
            Mock<IOddsProvider> oddsProvider = new Mock<IOddsProvider>();

            return oddsProvider.Object;
        }
    }
}