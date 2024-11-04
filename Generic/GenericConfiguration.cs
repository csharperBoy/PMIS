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
using static Generic.Base.Handler.SystemLog.WithSerilog.GenericLogWithSerilogHandlerFactory;
using Generic.DTO.Base.Handler.Log;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.DTO.Base.Handler.SystemLog.Serilog;

namespace Generic
{
    public static class GenericConfiguration
    {
        public static void ConfigureGenericLogServices(IServiceCollection _services, GenericConfigureLogWithSerilogRequestDto _req)
        {
            #region Log

            _services.AddSingleton<GenericLogWithSerilogInFileHandler>();
            _services.AddSingleton<GenericLogWithSerilogInSqlServerHandler>();
            _services.AddSingleton<GenericLogWithSerilogInFileHandler>();

            _services.AddSingleton<AbstractGenericLogWithSerilogHandler> (provider =>
            {
                return  GenericLogWithSerilogHandlerFactory.GetLogHandler(_req);
            });
            

            _services.AddSingleton<Func<GenericConfigureLogWithSerilogRequestDto, AbstractGenericLogWithSerilogHandler>>(serviceProvider => key =>
            {
                return GenericLogWithSerilogHandlerFactory.GetLogHandler(key);
            });


            _services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });

            //var logHandler = GenericLogWithSerilogHandlerFactory.GetLogHandler(_req);

            //  return logHandler;
            #endregion
        }
      
        public static void ConfigureGenericMapServices(IServiceCollection services)
        {

            #region Map
            services.AddScoped<GenericAutoMapHandler>();
            services.AddScoped<GenericManualMapHandler>();

            services.AddScoped<AbstractGenericMapHandler, GenericAutoMapHandler>();
            services.AddScoped<Func<MappingMode, AbstractGenericMapHandler>>(serviceProvider => key =>
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
