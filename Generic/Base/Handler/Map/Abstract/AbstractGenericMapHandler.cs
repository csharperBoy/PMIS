using AutoMapper;
using Generic.Base.Handler.Map.Contract;
using System.Linq.Expressions;

namespace Generic.Base.Handler.Map.Abstract
{
public abstract class AbstractGenericMapHandler : IGenericMapHandler
{
    public IConfigurationProvider ConfigurationProvider => throw new NotImplementedException();

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

    public delegate Task<TDestination> MappingHandler<TSource, TDestination>(TSource source, TDestination destination);
    public event MappingHandler<object, object> MappingEvent;

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
            catch (Exception)
            {
                throw;
            }
        }

        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            throw new NotImplementedException();
        }

        public IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object> parameters = null, params string[] membersToExpand)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TDestination>(object source)
        {
            throw new NotImplementedException();
        }

        TDestination IMapperBase.Map<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}
