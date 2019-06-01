namespace SportsBetting.Feeder.Core.Contracts.Mappers
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models.Base;
    using SportsBetting.Feeder.Models.Base;

    public interface IMapper<TFrom, To> 
        where TFrom : BaseFeedModel
        where To : BaseModel
    {
        To Map(TFrom from);

        IEnumerable<To> Map(IEnumerable<TFrom> from);
    }
}