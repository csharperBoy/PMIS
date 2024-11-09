using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Contract;

namespace Generic.Base.Handler.Map.Concrete
{
    public class GenericManualMapHandler : AbstractGenericMapHandler
    {
        public override Task AfterMap(Action<object, object> afterFunction)
        {
            throw new NotImplementedException();
        }

        public override Task BeforeMap(Action<object, object> beforeFunction)
        {
            throw new NotImplementedException();
        }

       // public async Task<TDestination> Map<TSource, TDestination>(TSource source)
       // {
            //    try
            //    {
            //        TEntityDestination destination = null ;
            //        var sourceFields = source.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            //        var destinationFields = destination.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //        //var matchingFields = fields1.Select(f => f.Name).Intersect(fields2.Select(f => f.Name)).Count();

            //        //return matchingFields;

            //        return destination;
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
        //    throw new NotImplementedException();
       // }

        public override Task<TDestination> Map<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException();
        }

        public override Task<TDestination> Map<TSource, TDestination>(TSource source, Action<IGenericMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public override Task Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }
    }
}
