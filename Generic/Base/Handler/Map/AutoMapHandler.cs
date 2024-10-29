using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Contract;
using AutoMapper;

namespace Generic.Base.Handler.Map
{
    public class AutoMapHandler : GenericMapHandler
    {
        public static async Task<TDestination> StaticMap<TSource, TDestination>(TSource source)
           where TDestination : class
           where TSource : class
        {
            IGenericMapHandler mapper = new AutoMapHandler();
            return await mapper.Map<TSource, TDestination>(source);
        }
        public override async Task<TDestination> Map<TSource, TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
            var mapper = new Mapper(config);
            TDestination destination = mapper.Map<TDestination>(source);
            destination = await ExtraMap(source, destination);
            return await Task.FromResult(destination);
        }
    }
}
