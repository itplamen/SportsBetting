﻿namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Mocks
{
    using HtmlAgilityPack;

    using Moq;

    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Models;

    public static class ServicesMock
    {
        public static IHtmlService GetHtmlService()
        {
            Mock<IHtmlService> htmlService = new Mock<IHtmlService>();

            htmlService.Setup(x => x.GetTwoWayOddsCount(
                    It.Is<HtmlNode>(y =>
                        y == null ||
                        y.InnerHtml.Contains("tableMarketRow__container___3jeni_three_odds_values"))))
                .Returns(0);

            htmlService.Setup(x => x.GetTwoWayOddsCount(
                    It.Is<HtmlNode>(y => 
                        y != null &&
                        y.InnerHtml.Contains("tableMarketRow__container___3jeni_one_odd"))))
                .Returns(1);

            htmlService.Setup(x => x.GetTwoWayOddsCount(
                    It.Is<HtmlNode>(y =>
                        y != null &&
                        y.InnerHtml.Contains("tableMarketRow__container___3jeni_two_odds"))))
                .Returns(2);

            htmlService.Setup(x => x.GetOddsCount(
                    It.Is<HtmlNode>(y =>
                        y != null &&
                        y.InnerHtml.Contains("tableMarketRow__container___3jeni_one_odd"))))
                .Returns(1);

            htmlService.Setup(x => x.GetOddsCount(
                    It.Is<HtmlNode>(y =>
                        y != null &&
                        y.InnerHtml.Contains("tableMarketRow__container___3jeni_handicap_with_values"))))
                .Returns(2);

            htmlService.Setup(x => x.GetOddsCount(
                    It.Is<HtmlNode>(y =>
                        y != null &&
                        (y.InnerHtml.Contains("tableMarketRow__container___3jeni_three_odds_values") ||
                         y.InnerHtml.Contains("tableMarketRow__container___3jeni_three_odds_no_values") ||
                         y.InnerHtml.Contains("tableMarketRow__container___3jeni_total_line_values") ||
                          y.InnerHtml.Contains("tableMarketRow__container___3jeni_total_line_without_header") ||
                          y.InnerHtml.Contains("tableMarketRow__container___3jeni_total_line_no_values") ||
                          y.InnerHtml.Contains("tableMarketRow__container___3jeni_total_line_suspended") ||
                          y.InnerHtml.Contains("tableMarketRow__container___3jeni_total_line_resulted")))))
                .Returns(3);

            htmlService.Setup(x => x.GetOddsCount(
                    It.Is<HtmlNode>(y =>
                        y != null &&
                        y.InnerHtml.Contains("tableMarketRow__container___3jeni_four_odds_values"))))
                .Returns(4);

            htmlService.Setup(x => x.HasHeader(
                    It.Is<HtmlNode>(y => 
                        y == null ||
                        y.InnerHtml.Contains("tableMarketRow__container___3jeni_total_line_without_header"))))
                .Returns(false);

            htmlService.Setup(x => x.HasHeader(
                    It.Is<HtmlNode>(y => 
                        y != null &&
                        (y.InnerHtml.Contains("tableMarketRow__specifier-value___3S715") ||
                         y.InnerHtml.Contains("tableMarketRow__container___3jeni_three_odds_values_header") ||
                         y.InnerHtml.Contains("tableMarketRow__container___3jeni_total_line_values") ||
                         y.InnerHtml.Contains("tableMarketRow__container___3jeni_total_line_no_values")))))
                .Returns(true);

            htmlService.Setup(x => x.IsSuspended(
                    It.Is<HtmlNode>(y =>
                        y != null &&
                        y.GetAttributeValue("title", string.Empty) == "Deactivated" &&
                        y.GetAttributeValue("class", string.Empty) == "__app-Odd-is-disabled odd__is-disabled___bCpHE")))
                .Returns(true);

            htmlService.Setup(x => x.GetOddResultStatus(
                    It.Is<HtmlNode>(y => 
                        y != null && 
                        y.InnerText.Contains("Win"))))
                .Returns(OddResultFeedStatus.Win);

            htmlService.Setup(x => x.GetOddResultStatus(
                    It.Is<HtmlNode>(y => 
                        y != null && 
                        y.InnerText.Contains("Loss"))))
                .Returns(OddResultFeedStatus.Loss);

            return htmlService.Object;
        }
    }
}