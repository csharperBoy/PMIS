using Generic.Base.Handler.Map.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.Map
{
    public class GenericMapHandler : AbstractGenericMapHandler
    {
        public static async Task<TDestination> StaticMap<TSource, TDestination>(TSource source)
           where TDestination : class
           where TSource : class
        {
            GenericMapHandler genericMapHandler = new GenericMapHandler();
            return await genericMapHandler.Map<TSource, TDestination>(source);
        }
    }
}
