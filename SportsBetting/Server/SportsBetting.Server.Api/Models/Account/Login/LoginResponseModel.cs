namespace SportsBetting.Server.Api.Models.Account.Login
{
    using System;

    using AutoMapper;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class LoginResponseModel : IMapFrom<Authentication>, IMapFrom<Account>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public decimal Balance { get; set; }

        public string LoginToken { get; set; }

        public DateTime Expiration { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Authentication, LoginResponseModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.AccountId))
                .ForMember(x => x.LoginToken, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Expiration, opt => opt.MapFrom(x => x.Expiration));

            configuration.CreateMap<Account, LoginResponseModel>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(x => x.Balance, opt => opt.MapFrom(x => x.Balance));
        }
    }
}