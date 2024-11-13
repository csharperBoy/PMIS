using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Contract;
using AutoMapper;
using System.Reflection;

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

        public override async Task<TDestination> Map<TSource, TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = new Mapper(config);
            var destination = Activator.CreateInstance<TDestination>();
            var mappingOptions = new GenericMappingOperationOptions();

            destination = await BeforeMap(source, destination);
            destination = mapper.Map<TSource, TDestination>(source, destination);
            destination = await AfterMap(source, destination);
            
            return await Task.FromResult(destination);
        }
        //public override void Map<TSource, TDestination>(TSource source, TDestination destination)
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<TSource, TDestination>();
        //    });
        //    var mapper = new Mapper(config);
        //    var mappingOptions = new GenericMappingOperationOptions();
        //    destination =  Map<TSource, TDestination>(source).Result;
            
        //}
        public override async Task Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
            var mapper = new Mapper(config);

            mapper.Map(source, destination);
           await Task.CompletedTask;
        }


        public async override Task<TDestination> Map<TSource, TDestination>(TSource source, Action<IGenericMappingOperationOptions> opts )
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
        public async Task<TDestination> BeforeMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class, new()
            where TSource : class, new()
        {
           // TSource result = new TSource();
            var mapMethodSource = typeof(TSource).GetMethod("BeforeMap", BindingFlags.Static | BindingFlags.Public);
            if (mapMethodSource != null)
            {
                var genericMethod = mapMethodSource.MakeGenericMethod(typeof(TSource), typeof(TDestination));

                var task = (Task)genericMethod.Invoke(null, new object[] { source, destination });
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    destination = (TDestination)resultProperty.GetValue(task);

                }

            }
            var mapMethodDestination = typeof(TDestination).GetMethod("BeforeMap", BindingFlags.Static | BindingFlags.Public);
            if (mapMethodDestination != null)
            {
                var genericMethod = mapMethodDestination.MakeGenericMethod(typeof(TSource), typeof(TDestination));

                var task = (Task)genericMethod.Invoke(null, new object[] { source, destination });
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    destination = (TDestination)resultProperty.GetValue(task);

                }

            }
            return destination;
        }
        public async Task<TDestination> AfterMap<TSource,TDestination>(TSource source, TDestination destination)
            where TDestination : class , new()
            where TSource : class , new()
        {
            //TDestination result = new TDestination();
            var mapMethodSource = typeof(TSource).GetMethod("AfterMap", BindingFlags.Static | BindingFlags.Public);
            if (mapMethodSource != null)
            {
                var genericMethod = mapMethodSource.MakeGenericMethod(typeof(TSource), typeof(TDestination));

                var task = (Task)genericMethod.Invoke(null, new object[] { source, destination });
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    destination = (TDestination)resultProperty.GetValue(task);

                }

            }
            var mapMethodDestination = typeof(TDestination).GetMethod("AfterMap", BindingFlags.Static | BindingFlags.Public);
            if (mapMethodDestination != null)
            {
                var genericMethod = mapMethodDestination.MakeGenericMethod(typeof(TSource), typeof(TDestination));

                var task = (Task)genericMethod.Invoke(null, new object[] { source, destination });
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    destination = (TDestination)resultProperty.GetValue(task);

                }

            }
            return destination;
        }

        
    }
}
