namespace SportsBetting.Handlers.Commands.Matches
{
    using AutoMapper;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Common.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateMatchCommand : ICommand, IMapFrom<MatchFeedModel>, IMapTo<Match>, IHaveCustomMappings
    {
        public int Key { get; set; }

        public string Score { get; set; }

        public string StreamURL { get; set; }

        public string TournamentId { get; set; }

        public string HomeTeamId { get; set; }

        public string AwayTeamId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MatchFeedModel, CreateMatchCommand>()
                .ForMember(x => x.Score, opt => opt.MapFrom(x => ScoreMapping.Map(x.HomeTeam.Score, x.AwayTeam.Score)));
        }
    }
}