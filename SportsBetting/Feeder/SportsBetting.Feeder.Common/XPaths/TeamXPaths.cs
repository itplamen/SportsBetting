namespace SportsBetting.Feeder.Common.XPaths
{
    public static class TeamXPaths
    {
        public const string NAME = ".//div[starts-with(@class,'TeamHeader__name___3cKcj')]";

        public const string HOME_TEAM_SCORE = ".//div[starts-with(@class,'LivematchHeader__home')]";

        public const string AWAY_TEAM_SCORE = ".//div[starts-with(@class,'LivematchHeader__away')]";
    }
}