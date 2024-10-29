namespace Generic.Base.Handler.Map.Contract
{
    public interface IGenericMapHandler
    {
        Task<TDestination> Map<TSource, TDestination>(TSource source)
            where TDestination : class
            where TSource : class;
    }
}
