using Generic.Base.Handler.Map.Abstract;

namespace Generic.Base.Handler.Map.Concrete
{
    public class GenericManualMapHandler : AbstractGenericMapHandler
    {
        public override async Task<TDestination> Map<TSource, TDestination>(TSource source)
        {
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
            throw new NotImplementedException();
        }
    }
}
