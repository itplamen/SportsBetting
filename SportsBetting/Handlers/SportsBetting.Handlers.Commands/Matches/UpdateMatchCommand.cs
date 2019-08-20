namespace SportsBetting.Handlers.Commands.Matches
{
    using System;

    using AutoMapper;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Common.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class UpdateMatchCommand : ICommand, IMapFrom<MatchFeedModel>, IMapTo<Match>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        public MatchStatus Status { get; set; }

        public string Score { get; set; }

        public string StreamURL { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MatchFeedModel, CreateMatchCommand>()
                .ForMember(x => x.Score, opt => opt.MapFrom(x => ScoreMapping.Map(x.HomeTeam.Score, x.AwayTeam.Score)));
        }       
    }
}