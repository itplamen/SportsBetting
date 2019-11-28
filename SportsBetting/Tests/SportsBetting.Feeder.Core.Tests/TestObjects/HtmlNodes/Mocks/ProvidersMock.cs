using Moq;
using SportsBetting.Feeder.Core.Contracts.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Mocks
{
    public static class ProvidersMock
    {
        public static IOddsProvider GetOddsProvider()
        {
            Mock<IOddsProvider> oddsProvider = new Mock<IOddsProvider>();

            return oddsProvider.Object;
        }
    }
}
