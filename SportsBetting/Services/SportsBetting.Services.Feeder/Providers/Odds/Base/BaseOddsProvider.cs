namespace SportsBetting.Services.Feeder.Providers.Odds.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Services;

    public abstract class BaseOddsProvider
    {
        private readonly IHtmlService htmlService;
        private readonly IObjectFactory objectFactory;

        public BaseOddsProvider(IHtmlService htmlService, IObjectFactory objectFactory)
        {
            this.htmlService = htmlService;
            this.objectFactory = objectFactory;
        }

        protected Odd BuildOdd(HtmlNode oddNode, string name, int rank, int marketId, string header = null)
        {
            decimal value = GetValue(oddNode);
            bool isSuspended = IsSuspended(oddNode);
            OddResultStatus resultStatus = htmlService.GetOddResultStatus(oddNode);

            return objectFactory.CreateOdd(name, value, isSuspended, resultStatus, rank, marketId, header);
        }

        protected virtual decimal GetValue(HtmlNode oddNode)
        {
            decimal parsedValue = 0;

            try
            {
                HtmlNodeCollection nodeCollection = oddNode.SelectNodes(OddXPaths.VALUE);

                if (nodeCollection != null)
                {
                    string value = nodeCollection.Select(x => x.InnerText).First();
                    decimal.TryParse(value, out parsedValue);
                }
            }
            catch (Exception ex)
            {
            }

            return parsedValue;
        }

        protected abstract bool ShouldGet(HtmlNode marketNode, IList<string> oddNames);

        protected abstract bool IsSuspended(HtmlNode oddNode);
    }
}