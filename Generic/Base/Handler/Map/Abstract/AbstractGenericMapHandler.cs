using Generic.Base.Handler.Map.Contract;

namespace Generic.Base.Handler.Map.Abstract
{
    public abstract class AbstractGenericMapHandler : IGenericMapHandler
    {
        public static async Task<TDestination> StaticMap<TMapper, TSource, TDestination>(TMapper mapper, TSource source)
           where TDestination : class
           where TSource : class
        {
            try
            {
                var subclassType = mapper.GetType();
                var instance = Activator.CreateInstance(subclassType) as IGenericMapHandler;

                if (instance == null)
                {
                    throw new InvalidOperationException("Could not create instance of subclass.");
                }

                return await instance.Map<TSource, TDestination>(source);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public abstract Task<TDestination> Map<TSource, TDestination>(TSource source)
           where TDestination : class
           where TSource : class;

        public event Func<object, object, Task> MapEvent;
        public virtual async Task<TDestination> ExtraMap<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            try
            {
                if (MapEvent != null)
                {
                    await MapEvent.Invoke(source, destination);
                }
                return await Task.FromResult(destination);
            }
            catch (Exception)
            {
                throw;
            }
        }









        public delegate Task MappingHandler<TSource, TDestination>(TSource source, TDestination destination);
        public event MappingHandler<object, object> MappingEvent;

        public async Task<TDestination> Mapping<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            Console.WriteLine("Mapping is being performed...");

            // فراخوانی رویداد در صورت وجود  
            if (MappingEvent != null)
            {
                await MappingEvent.Invoke(source, destination);
            }

            return destination;
        }
    }
}
