using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Contract;
using AutoMapper;

namespace Generic.Base.Handler.Map
{
    public class GenericAutoMapHandler : AbstractGenericMapHandler
    {
        public static async Task StaticMap<TSource, TDestination>(TSource source)
           where TDestination : class
           where TSource : class
        {
            IGenericMapHandler mapper = new GenericAutoMapHandler();
            await mapper.Map<TSource, TDestination>(source);
        }
        public override async Task<TDestination> Map<TSource, TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
            var mapper = new Mapper(config);
            TDestination destination = Activator.CreateInstance<TDestination>();
            destination = mapper.Map<TDestination>(source);
            return await ExtraMap(source, destination); 
        }
        //public override Task<bool> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //{
        //    return base.ExtraMap<TSource, TDestination>(source, destination);
        //}
    }
}
