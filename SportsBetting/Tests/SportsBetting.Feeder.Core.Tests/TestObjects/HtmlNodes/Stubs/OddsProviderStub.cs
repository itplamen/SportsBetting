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

        public static HtmlNode GetThreeWayMarketNodeWithValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_three_odds_values'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
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

        public static HtmlNode GetThreeWayMarketNodeWithMoreThanThreeOddsAndValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_four_odds_values'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
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

        public static HtmlNode GetThreeWayMarketNodeWithoutValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_three_odds_no_values'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'></div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetThreeWayMarketNodeWithHeaders()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_three_odds_values_header'>
                            <div title='1' class='tableMarketRow__specifier-value___3S715'>1</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
                            <div title='2' class='tableMarketRow__specifier-value___3S715'>2</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
                            <div title='3' class='tableMarketRow__specifier-value___3S715'>3</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div>2.23</div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetThreeWayMarketNodeWithOneSuspendedOdd()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_three_odds_values'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div title='Deactivated' class='__app-Odd-is-disabled odd__is-disabled___bCpHE'></div>
                            </div>
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

        public static HtmlNode GetThreeWayMarketNodeWithThreeResultedOdds()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_three_odds_values'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>Win</div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>Loss</div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>Loss</div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTotalLineMarketNodeWithValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_total_line_values'>
                            <div title='3.5' class='tableMarketRow__specifier-value___3S715'>3.5</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>2.32</div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>1.33</div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTotalLineMarketNodeWithoutHeader()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_total_line_without_header'>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>2.32</div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>1.33</div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTotalLineMarketNodeWithoutValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_total_line_no_values'>
                            <div title='3.5' class='tableMarketRow__specifier-value___3S715'>3.5</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTotalLineMarketNodeWithSuspendedOdd()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_total_line_suspended'>
                            <div title='3.5' class='tableMarketRow__specifier-value___3S715'>3.5</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>2.25</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div title='Deactivated' class='__app-Odd-is-disabled odd__is-disabled___bCpHE'></div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetTotalLineMarketNodeWithResultedOdds()
        {
            string html = @"
                <div>
                    <div>
                        <div class='tableMarketRow__container___3jeni_total_line_resulted'>
                            <div title='3.5' class='tableMarketRow__specifier-value___3S715'>3.5</div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>Loss</div>
                            </div>
                            <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                <div class='odd__result___1K5vV'>Win</div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }
    }
}