namespace SportsBetting.Server.Api.Models.Matches
{
    using System.Collections.Generic;

    using AutoMapper;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Server.Api.Models.Markets;

    public class MatchResponseModel : IMapFrom<MatchResult>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Score { get; set; }

        public string Type { get; set; }

        public string Tournament { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public IEnumerable<MarketResponseModel> Markets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MatchResult, MatchResponseModel>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Type.ToString()));
        }
    }
}