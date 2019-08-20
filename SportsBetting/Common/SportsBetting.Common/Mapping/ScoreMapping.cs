namespace SportsBetting.Common.Mapping
{
    public static class ScoreMapping
    {
        public static string Map(int? homeScore, int? awayScore)
        {
            if (homeScore != null && awayScore != null)
            {
                return $"{homeScore}:{awayScore}";
            }

            return null;
        }
    }
}