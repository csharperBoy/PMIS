using AutoMapper;
using System.Linq.Expressions;

namespace Generic.Base.Handler.Map.Contract
{
    public interface IGenericMapHandler : IMapper
    {
        Task<TDestination> Map<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class;


        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts);

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts);

        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts);

        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts);

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand);

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand);

        public IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object> parameters = null, params string[] membersToExpand);

        public TDestination Map<TDestination>(object source);

       // TDestination IMapperBase.Map<TSource, TDestination>(TSource source);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        public object Map(object source, Type sourceType, Type destinationType);

        public object Map(object source, object destination, Type sourceType, Type destinationType);
    }
}
