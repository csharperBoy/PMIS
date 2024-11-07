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
           // destination =await mapper.Map<TSource, TDestination>(source);
        }

        public delegate Task<TDestination> MappingHandler<TSource, TDestination>(TSource source, TDestination destination);
        public event MappingHandler<object, object> MappingEvent;


        public async override Task<TDestination> Map<TSource, TDestination>(TSource source, Action<IGenericMappingOperationOptions> opts = null)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = new Mapper(config);
            var destination = Activator.CreateInstance<TDestination>();
            var mappingOptions = new GenericMappingOperationOptions();
            opts?.Invoke(mappingOptions); 
            if (mappingOptions.BeforeMapAction != null)
            {
                mappingOptions.BeforeMapAction(source, destination);
            }
            destination = mapper.Map<TSource, TDestination>(source);
            if (mappingOptions.AfterMapAction != null)
            {
                mappingOptions.AfterMapAction(source, destination);
            }
            return await Task.FromResult(destination);
        }

        public override Task BeforeMap(Action<object, object> beforeFunction)
        {
            throw new NotImplementedException();
        }

        public override Task AfterMap(Action<object, object> afterFunction)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            try
            {
                if (MappingEvent != null)
                {
                    await MappingEvent.Invoke(source, destination);
                }

                return await Task.FromResult(destination);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<TDestination> Map<TSource, TDestination>(TSource source)
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
        public async Task<TDestination> Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
            var mapper = new Mapper(config);
            TDestination destination = Activator.CreateInstance<TDestination>();
            destination = mapper.Map<TSource, TDestination>(source, opts);
            // return await ExtraMap(source, destination);
            //if (MappingEvent != null)
            //{
            //   await a<TSource, TDestination>.Invoke(source,destination);
            //}

            return await Task.FromResult(destination);
        }

        //public Task<bool> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
        //{
        //    return base.ExtraMap<TSource, TDestination>(source, destination);
        //}
    }
}
