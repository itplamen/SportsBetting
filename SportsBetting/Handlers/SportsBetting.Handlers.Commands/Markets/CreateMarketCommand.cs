namespace SportsBetting.Handlers.Commands.Markets
{
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateMarketCommand : ICommand, IMapFrom<MarketFeedModel>, IMapTo<Market>, IHaveCustomMappings
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string MatchId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MarketFeedModel, CreateMarketCommand>()
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Odds.Select(y => (int)y.Type).FirstOrDefault()));

            configuration.CreateMap<CreateMarketCommand, Market>()
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Type));
        }
    }
}