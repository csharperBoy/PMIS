using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Contract;
using AutoMapper;

namespace Generic.Base.Handler.Map.Concrete
{
    public class GenericAutoMapHandler : AbstractGenericMapHandler
    {
        public static async void StaticMap<TSource, TDestination>(TSource source,TDestination destination)
           where TDestination : class
           where TSource : class
        {
            IGenericMapHandler mapper = new GenericAutoMapHandler();
            destination =await mapper.Map<TSource, TDestination>(source);
        }

        public delegate Task<TDestination> MappingHandler<TSource, TDestination>(TSource source, TDestination destination);
        public event MappingHandler<object, object> MappingEvent;

        //public virtual async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //    where TSource : class
        //    where TDestination : class
        //{
        //    try
        //    {
        //        if (MappingEvent != null)
        //        {
        //            await MappingEvent.Invoke(source, destination);
        //        }

        //        return await Task.FromResult(destination);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public override async Task<TDestination> Map<TSource, TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
            var mapper = new Mapper(config);
            TDestination destination = Activator.CreateInstance<TDestination>();
            destination = mapper.Map<TDestination>(source);
            // return await ExtraMap(source, destination);
            if (MappingEvent != null)
            {
                await MappingEvent.Invoke(source, destination);
            }

            return await Task.FromResult(destination);
        }
        public override async Task<TDestination> Map<TSource, TDestination>(TSource source,  Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
            var mapper = new Mapper(config);
            TDestination destination = Activator.CreateInstance<TDestination>();
            destination = mapper.Map<TSource,TDestination>(source,opts);
            // return await ExtraMap(source, destination);
            //if (MappingEvent != null)
            //{
            //   await a<TSource, TDestination>.Invoke(source,destination);
            //}

            return await Task.FromResult(destination);
        }
        //public override Task<bool> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //{
        //    return base.ExtraMap<TSource, TDestination>(source, destination);
        //}
    }
}
