namespace SportsBetting.Server.Api.Extensions
{
    using AutoMapper;

    public static class MapperExtensions
    {
        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
        {
            return Mapper.Map(source, destination);
        }
    }
}