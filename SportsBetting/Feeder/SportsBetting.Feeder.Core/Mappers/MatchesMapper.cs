namespace SportsBetting.Feeder.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Mappers;
    using SportsBetting.Feeder.Models;

    public class MatchesMapper : IMapper<MatchFeedModel, Match>
    {
        public Match Map(MatchFeedModel from)
        {
            Match match = new Match()
            {
                Key = from.Id,
                StartTime = from.StartTime,
                Type = MapType(from.IsLive),
                Status = MapStatus(from.Status),
                Score = MapScore(from.HomeTeam, from.AwayTeam),
            };

            return match;
        }

        public IEnumerable<Match> Map(IEnumerable<MatchFeedModel> from)
        {
            ICollection<Match> matches = new List<Match>();

            foreach (var matchFeed in from)
            {
                matches.Add(Map(matchFeed));
            }

            return matches;
        }

        private MatchType MapType(bool isLive)
        {
            if (isLive)
            {
                return MatchType.Live;
            }

            return MatchType.Prematch;
        }

        private MatchStatus MapStatus(MatchFeedStatus feedStatus)
        {
            switch (feedStatus)
            {
                case MatchFeedStatus.NotStarted:
                    return MatchStatus.NotStarted;
                case MatchFeedStatus.InPlay:
                    return MatchStatus.InPlay;
                case MatchFeedStatus.Ended:
                    return MatchStatus.Ended;
                case MatchFeedStatus.Suspended:
                    return MatchStatus.Suspended;
                case MatchFeedStatus.Abandoned:
                    return MatchStatus.Abandoned;
                case MatchFeedStatus.Closed:
                    return MatchStatus.Closed;
                case MatchFeedStatus.Cancelled:
                    return MatchStatus.Cancelled;
                case MatchFeedStatus.Delayed:
                    return MatchStatus.Delayed;
                default:
                    throw new ArgumentOutOfRangeException($"Invalid match feed status - {feedStatus.ToString()}");
            }
        }

        private string MapScore(TeamFeedModel homeTeam, TeamFeedModel awayTeam)
        {
            if (homeTeam.Score.HasValue && awayTeam.Score.HasValue)
            {
                return $"{homeTeam.Score}:{awayTeam.Score}";
            }

            return null;
        }
    }
}