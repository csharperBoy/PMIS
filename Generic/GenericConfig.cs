using Generic.Base.Handler.Log.Abstract;
using Generic.Base.Handler.Log.Concrete;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Repository.Abstract;
using Generic.Repository;
using Generic.Service.Normal.Composition.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Operation.Abstract;
using Generic.Service.Normal.Operation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace Generic
{
    public static class GenericConfig
    {
        public static void ConfigureGenericServices(IServiceCollection services)
        {
            #region Log
            services.AddSingleton<AbstractGenericLogHandler, GenericLogHandler>();
            #endregion
            #region Map
            services.AddSingleton<GenericAutoMapHandler>();
            services.AddSingleton<GenericManualMapHandler>();

            services.AddSingleton<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddSingleton<Func<MappingMode, AbstractGenericMapHandler>>(serviceProvider => key =>
            {
                return GetMapper(serviceProvider, key);
            });
            #endregion
            #region SystemException
            services.AddSingleton<AbstractGenericExceptionHandler, GenericMyExceptionHandler>();

            #endregion

        }
    }
}
