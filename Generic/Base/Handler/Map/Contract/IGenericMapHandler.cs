using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.Map.Contract
{
    internal interface IGenericMapHandler
    {
        Task<TDestination> Map<TSource, TDestination>(TSource source)
            where TDestination : class
            where TSource : class;

        
    }
}
