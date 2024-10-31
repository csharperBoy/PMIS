using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.Map
{    public static class MapHandlerFactory
    {
        public static AbstractGenericMapHandler GetMapper(bool useAutoMapping)
        {
            if (useAutoMapping)
            {
                return new GenericAutoMapHandler();
            }
            else
            {
                return new GenericManualMapHandler();
            }
        }
    }
}
