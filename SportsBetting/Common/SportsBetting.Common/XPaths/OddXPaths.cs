namespace SportsBetting.Common.XPaths
{
    using System.Collections.Generic;

    public static class OddXPaths
    {
        // Odd result status

        public const string WIN_STATUS = "tableMarketRow__win___2JVc_";

        public const string LOSS_STATUS = "tableMarketRow__loss___gU8ll";

        public const string HANDICAP_WIN_STATUS = "marketRowOddTable__win___1AXuJ";

        public const string HANDICAP_LOSS_STATUS = "marketRowOddTable__loss___1c1Uh";

        public const string CORRECT_SCORE_WIN_STATUS = "scoreMarketRow__win___3bZc-";

        public const string CORRECT_SCORE_LOSS_STATUS = "scoreMarketRow__loss___B6qsc";


        // Odd info

        public const string NAME = ".//div[starts-with(@class, 'tableColumnNames__column___-zFNK')]";

        public const string VALUE = ".//button[starts-with(@class,'__app-Odd-button odd__button___2eiZg')]";

        public const string HEADER = ".//div[starts-with(@class, 'tableMarketRow__specifier-value___3S715')]";

        public static readonly string NODE = $".//div[starts-with(@class, 'tableMarketRow__odd___36mWy') or contains(@class, '{WIN_STATUS}') or contains(@class, '{LOSS_STATUS}')]";

        public static readonly IEnumerable<string> SUSPENDED = new List<string>() { "__app-Odd-is-disabled", "odd__is-disabled___bCpHE" };

        public const string TWO_WAY_COUNT = ".//div[starts-with(@class, 'tableMarketRow__odd-wrapper___3nnKr tableMarketRow__two-column___1j_fW')]";

        public const string HANDICAP_ROW = ".//div[starts-with(@class, 'marketRowOddTable__value___ptzs9')]";

        public const string CORRECT_SCORE_VALUE = ".//div[starts-with(@class, 'scoreMarketRow__odd-wrapper___3g5qT scoreMarketRow__three-column___3WDFU')]";

        public static readonly IEnumerable<string> CORRECT_SCORE_COLUMNS = new List<string>()
        {
            ".//div[starts-with(@class, 'scoreMarketRow__competitor-column___3KrdY scoreMarketRow__first___3ppxu')]",
            ".//div[starts-with(@class, 'scoreMarketRow__competitor-column___3KrdY scoreMarketRow__third___3yK6M')]"
        };
    }
}