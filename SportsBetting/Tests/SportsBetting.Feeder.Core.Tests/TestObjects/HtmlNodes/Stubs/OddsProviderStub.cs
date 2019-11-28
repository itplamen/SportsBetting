namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs
{
    using HtmlAgilityPack;

    public static class OddsProviderStub
    {
        public static HtmlNode GetMarketNodeWithOneOdd()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_one_odd'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTwoWayMarketNodeWithHeaders()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_two_odds'>
                            <div title='1' class='tableMarketRow__specifier-value___3S715'>1</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                            <div title='2' class='tableMarketRow__specifier-value___3S715'>2</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTwoWayMarketOddWithoutValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_two_odds'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTwoWayMarketNodeWithOneSuspendedOdd()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_two_odds_suspended'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div title='Deactivated' class='__app-Odd-is-disabled odd__is-disabled___bCpHE'></div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTwoWayMarketNodeWithTwoResultedOdds()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_two_odds'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>Win</div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>Loss</div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTwoWayMarketNodeWithValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_two_odds'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetThreeWayMarketNode()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_three_odd'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }
    }
}