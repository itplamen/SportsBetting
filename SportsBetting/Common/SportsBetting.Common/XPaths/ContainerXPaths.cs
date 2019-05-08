namespace SportsBetting.Common.XPaths
{
    public static class ContainerXPaths
    {
        public const string BETTING_CONTAINER = ".//main[@id='betting__container']";

        public const string MARKET = ".//div[starts-with(@class, 'tableMarketRow__container___3jeni')]";

        public const string THREE_WAY_ODDS = ".//div[starts-with(@class, 'tableMarketRow__three-odds-container___1J1o4 tableMarketRow__horizontal___2w44O')]";

        public const string HANDICAP_ODDS = ".//div[starts-with(@class, 'marketRowOddTable__container___TfWHU')]";
    }
}