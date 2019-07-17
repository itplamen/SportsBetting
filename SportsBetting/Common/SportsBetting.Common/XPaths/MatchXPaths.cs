namespace SportsBetting.Common.XPaths
{
    public static class MatchXPaths
    {
        public const string MARKETS = ".//div[starts-with(@class,'__app-TableGroupMarket-table marketTable__table___dvHTz')]";

        public const string HEADER_INFO_BOX = ".//div[starts-with(@class,'MatchHeaderInfobox')]";

        public const string EVENT_BODY = ".//div[contains(@class, 'sportEventRow__body___3Ywcg')]";

        public const string EVENT_URL = "//a[starts-with(@href,'/en/betting/match/5:')]";
    }
}