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

        public static HtmlNode GetHandicapMarketNodeWithValues()
        {
            string html = @"
                <div>
                    <div>
                        <div>
                            <div class='marketTable__header___mSHxT' title='Map handicap'>Map handicap</div>
                        </div>
                        <div class='tableMarketRow__container___3jeni_handicap_with_values'>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='-1.5'>
                                        <span>-1.5</span>
                                    </div>
                                    <div class='marketRowOddTable__odd-wrapper___lWDBZ'>
                                        <button class='__app-Odd-button odd__button___2eiZg'>5.18</button>
                                    </div>
                                </div>
                            </div>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='1.5'>
                                        <span>1.5</span>
                                    </div>
                                    <div class='marketRowOddTable__odd-wrapper___lWDBZ'>
                                        <button class='__app-Odd-button odd__button___2eiZg'>1.18</button>
                                    </div>
                                </div>
                            </div>
                        </div>                       
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetHandicapMarketNodeWithMissingMarketName()
        {
            string html = @"
                <div>
                    <div>
                        <div>
                            <div class='marketTable__header___mSHxT' title='Map handicap'>Test Name</div>
                        </div>
                        <div class='tableMarketRow__container___3jeni_handicap_with_values'>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='-1.5'>
                                        <span>-1.5</span>
                                    </div>
                                    <div class='marketRowOddTable__odd-wrapper___lWDBZ'>
                                        <button class='__app-Odd-button odd__button___2eiZg'>5.18</button>
                                    </div>
                                </div>
                            </div>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='1.5'>
                                        <span>1.5</span>
                                    </div>
                                    <div class='marketRowOddTable__odd-wrapper___lWDBZ'>
                                        <button class='__app-Odd-button odd__button___2eiZg'>1.18</button>
                                    </div>
                                </div>
                            </div>
                        </div>                       
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetHandicapMarketNodeWithoutValues()
        {
            string html = @"
                <div>
                    <div>
                        <div>
                            <div class='marketTable__header___mSHxT' title='Map handicap'>Map handicap</div>
                        </div>
                        <div class='tableMarketRow__container___3jeni_handicap_with_values'>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='-1.5'>
                                        <span>-1.5</span>
                                    </div>
                                </div>
                            </div>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='1.5'>
                                        <span>1.5</span>
                                    </div>
                                </div>
                            </div>
                        </div>                       
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetHandicapMarketNodeWithSuspendedOdd()
        {
            string html = @"
                <div>
                    <div>
                        <div>
                            <div class='marketTable__header___mSHxT' title='Map handicap'>Map handicap</div>
                        </div>
                        <div class='tableMarketRow__container___3jeni_handicap_with_values'>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='-1.5'>
                                        <span>-1.5</span>
                                    </div>
                                    <div class='marketRowOddTable__odd-wrapper___lWDBZ'>
                                        <div title='Deactivated' class='__app-Odd-is-disabled odd__is-disabled___bCpHE'></div>
                                    </div>
                                </div>
                            </div>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='-1.5'>
                                        <span>-1.5</span>
                                    </div>
                                    <div class='marketRowOddTable__odd-wrapper___lWDBZ'>
                                        <button class='__app-Odd-button odd__button___2eiZg'>1.18</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetHandicapMarketNodeWithResultedOdd()
        {
            string html = @"
                <div>
                    <div>
                        <div>
                            <div class='marketTable__header___mSHxT' title='Map handicap'>Map handicap</div>
                        </div>
                        <div class='tableMarketRow__container___3jeni_handicap_with_values'>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='-1.5'>
                                        <div>-1.5</div>
                                    </div>
                                    <div class='marketRowOddTable__odd-wrapper___lWDBZ'>
                                        <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                            <div class='odd__result___1K5vV'>Win</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class='tableMarketRow__column___2kCi8'>
                                <div class='marketRowOddTable__container___TfWHU'>
                                    <div class='marketRowOddTable__title-wrapper___1JryI' title='-1.5'>
                                        <div>-1.5</div>
                                    </div>
                                    <div class='marketRowOddTable__odd-wrapper___lWDBZ'>
                                        <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                            <div class='odd__result___1K5vV'>Loss</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetCorrectScoreMarketNodeWithValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='marketTable__header___mSHxT' title='Correct map score'>Correct map score</div>
                    </div>
                    <div class='scoreMarketRow__container___1n55P_correct_score_with_values'>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='2:1'>2:1</div>
                                    <button class='__app-Odd-button odd__button___2eiZg'>
                                        <div class='__app-Odd-inner odd__inner___3F_cm'>
                                            <div class='odd__ellipsis___3b4Yk'>2.25</div>
                                        </div>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='1:2'>1:2</div>
                                    <button class='__app-Odd-button odd__button___2eiZg'>
                                        <div class='__app-Odd-inner odd__inner___3F_cm'>
                                            <div class='odd__ellipsis___3b4Yk'>1.6</div>
                                        </div>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetCorrectScoreMarketNodeWithoutMarketName()
        {
            string html = @"
                <div>
                    <div>
                        <div class='marketTable__header___mSHxT' title='Correct map score'>Test Name</div>
                    </div>
                    <div class='scoreMarketRow__container___1n55P_correct_score_with_values'>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='2:1'>2:1</div>
                                    <button class='__app-Odd-button odd__button___2eiZg'>
                                        <div class='__app-Odd-inner odd__inner___3F_cm'>
                                            <div class='odd__ellipsis___3b4Yk'>2.25</div>
                                        </div>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='1:2'>1:2</div>
                                    <button class='__app-Odd-button odd__button___2eiZg'>
                                        <div class='__app-Odd-inner odd__inner___3F_cm'>
                                            <div class='odd__ellipsis___3b4Yk'>1.6</div>
                                        </div>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetCorrectScoreMarketNodeWithoutValues()
        {
            string html = @"
                <div>
                    <div>
                        <div class='marketTable__header___mSHxT' title='Correct map score'>Correct map score</div>
                    </div>
                    <div class='scoreMarketRow__container___1n55P_correct_score_with_values'>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='2:1'>2:1</div>
                                </div>
                            </div>
                        </div>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='1:2'>1:2</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetCorrectScoreMarketNodeWithOneSuspendedOdd()
        {
            string html = @"
                <div>
                    <div>
                        <div class='marketTable__header___mSHxT' title='Correct map score'>Correct map score</div>
                    </div>
                    <div class='scoreMarketRow__container___1n55P_correct_score_with_values'>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='2:1'>2:1</div>
                                    <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                        <div title='Deactivated' class='__app-Odd-is-disabled odd__is-disabled___bCpHE'></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='1:2'>1:2</div>
                                    <button class='__app-Odd-button odd__button___2eiZg'>
                                        <div class='__app-Odd-inner odd__inner___3F_cm'>
                                            <div class='odd__ellipsis___3b4Yk'>1.6</div>
                                        </div>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetCorrectScoreMarketNodeWithOneResultedOdd()
        {
            string html = @"
                <div>
                    <div>
                        <div class='marketTable__header___mSHxT' title='Correct map score'>Correct map score</div>
                    </div>
                    <div class='scoreMarketRow__container___1n55P'>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='2:1'>2:1</div>
                                    <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                        <div class='odd__result___1K5vV'>Win</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class='scoreMarketRow__column___2OuUC'>
                            <div class='scoreMarketRow__odd-wrapper___3g5qT'>
                                <div class='__app-Odd-title odd__title___1kZ6V' title='1:2'>1:2</div>
                                    <div class='tableMarketRow__odd-wrapper___3nnKr'>
                                        <div class='odd__result___1K5vV'>Loss</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }
    }
}