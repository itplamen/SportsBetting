namespace SportsBetting.Common.Contracts
{
    using System.Collections.Generic;

    public interface IMapper<TFrom, To> 
        where TFrom : class
        where To : class
    {
        To Map(TFrom from);

        IEnumerable<To> Map(IEnumerable<TFrom> from);
    }
}