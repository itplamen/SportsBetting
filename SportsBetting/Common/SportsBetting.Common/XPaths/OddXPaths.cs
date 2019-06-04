namespace SportsBetting.Common.XPaths
{
    using System.Collections.Generic;

    public static class OddXPaths
    {
        public const string NAMES = ".//div[starts-with(@class, 'tableColumnNames__column___-zFNK')]";

        public const string VALUE = ".//button[starts-with(@class,'__app-Odd-button odd__button___2eiZg')]";

        public const string NODE = ".//div[starts-with(@class, 'tableMarketRow__odd-wrapper___3nnKr')]";

        public const string HANDICAP_NODE = ".//div[starts-with(@class, 'marketRowOddTable__odd-wrapper___lWDBZ')]";

        public const string RESULTED_STATUS = ".//div[starts-with(@class, 'odd__result___1K5vV')]";

        public const string CORRECT_SCORE_NODE = ".//div[starts-with(@class, 'scoreMarketRow__column___2OuUC')]";

        public const string HEADER = ".//div[starts-with(@class, 'tableMarketRow__specifier-value___3S715')]";

        public static readonly IEnumerable<string> SUSPENDED = new List<string>() { "__app-Odd-is-disabled", "odd__is-disabled___bCpHE" };
    }
}