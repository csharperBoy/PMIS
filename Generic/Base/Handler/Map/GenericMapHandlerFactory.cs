using AutoMapper;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Base.Handler.Map
{    public static class GenericMapHandlerFactory
    {
        private static AbstractGenericMapHandler autoMapper;
        private static AbstractGenericMapHandler manualMapper;

        public enum MappingMode
        {
            Auto,
            Manual
        }

        public static AbstractGenericMapHandler GetMapper(MappingMode mappingMode)
        {
            switch (mappingMode)
            {
                case MappingMode.Auto:
                    if (autoMapper == null)
                    {
                        autoMapper = new GenericAutoMapHandler();
                    }
                    return autoMapper;

                case MappingMode.Manual:
                    if (manualMapper == null)
                    {
                        manualMapper = new GenericManualMapHandler();
                    }
                    return manualMapper;

                default:
                    throw new ArgumentException("Invalid mapping mode");
            }
        }
        public static AbstractGenericMapHandler GetMapper(IServiceProvider serviceProvider, MappingMode mappingMode)
        {
            switch (mappingMode)
            {
                case MappingMode.Auto:
                    return serviceProvider.GetService<GenericAutoMapHandler>();
                case MappingMode.Manual:
                    return serviceProvider.GetService<GenericManualMapHandler>();
                default:
                    throw new ArgumentException("Invalid mapping mode");
            }
        }
    }
}
