namespace Generic.Base.Handler.Map.Contract
{
    public interface IGenericMapHandler
    {
        Task<TDestination> Map<TSource, TDestination>(TSource source, Action<IGenericMappingOperationOptions> opts = null)
            where TSource : class, new()
            where TDestination : class, new();
    }
}
