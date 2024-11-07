using Generic.Base.Handler.Map.Contract;

namespace Generic.Base.Handler.Map.Abstract
{
    public abstract class AbstractGenericMapHandler : IGenericMapHandler, IGenericMappingOperationOptions
    {
        public abstract Task<TDestination> Map<TSource, TDestination>(TSource source)
            where TSource : class, new()
            where TDestination : class, new();
        
        public abstract Task<TDestination> Map<TSource, TDestination>(TSource source, Action<IGenericMappingOperationOptions> opts)
            where TSource : class, new()
            where TDestination : class, new();
       
        public abstract Task BeforeMap(Action<object, object> beforeFunction);

        public abstract Task AfterMap(Action<object, object> afterFunction);
        
    }
}
