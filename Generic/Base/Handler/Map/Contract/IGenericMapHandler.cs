namespace Generic.Base.Handler.Map.Contract
{
    public interface IGenericMapHandler
    {
        Task<TDestination> Map<TSource, TDestination>(TSource source)
            where TSource : class, new()
            where TDestination : class, new();

        void Map<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class, new()
            where TDestination : class, new();

        Task<TDestination> Map<TSource, TDestination>(TSource source, Action<IGenericMappingOperationOptions> opts)
           where TSource : class, new()
           where TDestination : class, new();
    }
}
