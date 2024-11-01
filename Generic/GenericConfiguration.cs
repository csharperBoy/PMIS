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
using Serilog;
using Microsoft.Extensions.Logging;
using Generic.Service.ShapeExample;
using static Generic.Base.Handler.SystemLog.GenericLogHandlerFactory;
using Generic.DTO.Base.Handler.Log;
using Generic.Base.Handler.SystemLog;
using Generic.Base.Handler.SystemLog.Abstract;
using Generic.Base.Handler.SystemLog.Concrete;

namespace Generic
{
    public static class GenericConfiguration
    {
        public static AbstractGenericLogHandler ConfigureGenericLogServices(IServiceCollection _services, GenericConfigureLogRequestDto _req)
        {
            #region Log
            _services.AddSingleton<AbstractGenericLogHandler, GenericLogInFileHandler>();

            _services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });

            var logHandler = GenericLogHandlerFactory.GetLogHandler(_req);

            return logHandler;
            #endregion
        }
        public static void ConfigureGenericMapServices(IServiceCollection services)
        {

            #region Map
            services.AddSingleton<GenericAutoMapHandler>();
            services.AddSingleton<GenericManualMapHandler>();

            services.AddSingleton<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddSingleton<Func<MappingMode, AbstractGenericMapHandler>>(serviceProvider => key =>
            {
                return GetMapper(serviceProvider, key);
            });
            #endregion

        }

        public static void ConfigureGenericSystemExceptionServices(IServiceCollection services)
        {
            #region SystemException
            services.AddSingleton<AbstractGenericExceptionHandler, GenericMyExceptionHandler>();
            #endregion

        }
    }
}
