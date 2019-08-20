namespace SportsBetting.Feeder.Core.Providers.Odds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.Contracts;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Models;

    public class LoggingOddsProvider : IOddsProvider
    {
        private const string LOGGER = nameof(LoggingOddsProvider);

        private readonly ILogger logger;
        private readonly string oddsProviderName;
        private readonly IOddsProvider decoratedOdssProvider;

        public LoggingOddsProvider(IOddsProvider decoratedOdssProvider, ILoggerFactory loggerFactory)
        {
            this.decoratedOdssProvider = decoratedOdssProvider;
            this.oddsProviderName = decoratedOdssProvider.GetType().Name;
            this.logger = loggerFactory.Create($"{LOGGER}.txt", "Odd Providers");
        }

        public IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames, int marketKey)
        {
            try
            {
                return decoratedOdssProvider.Get(marketNode, oddNames, marketKey);
            }
            catch (Exception ex)
            {
                logger.LogError($"{oddsProviderName} failed", ex);
            }

            return Enumerable.Empty<OddFeedModel>();
        }
    }
}