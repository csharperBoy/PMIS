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
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog.DTO;
using static Generic.Base.Handler.Map.GenericExceptionHandlerFactory;

namespace Generic
{
    public static class GenericConfiguration
    {
        public static void ConfigureGenericLogServices(IServiceCollection services, GenericConfigureLogWithSerilogRequestDto req)
        {
            #region Log

            services.AddSingleton<GenericLogWithSerilogInFileHandler>();
            services.AddSingleton<GenericLogWithSerilogInSqlServerHandler>();
            services.AddSingleton<AbstractGenericLogWithSerilogHandler> (serviceProvider =>
            {
                return  GetLogHandler(req);
            });
            services.AddSingleton<Func<GenericConfigureLogWithSerilogRequestDto, AbstractGenericLogWithSerilogHandler>>(serviceProvider => key =>
            {
                return GetLogHandler(serviceProvider, key);
            });


            services.AddLogging(loggingBuilder =>
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
            services.AddScoped<AbstractGenericMapHandler>(serviceProvider =>
            {
                return GetMapper(serviceProvider, MappingMode.Auto);
            });
            services.AddScoped<Func<MappingMode, AbstractGenericMapHandler>>(serviceProvider => key =>
            {
                return GetMapper(serviceProvider, key);
            });
            #endregion

        }

        public static void ConfigureGenericSystemExceptionServices(IServiceCollection services)
        {
            #region SystemException
            services.AddScoped<GenericExceptionHandler>();
            services.AddScoped<AbstractGenericExceptionHandler>(serviceProvider =>
            {
                return GetExceptionHandler(serviceProvider, ExceptionHandler.Generic);
            });
            services.AddScoped<Func<ExceptionHandler, AbstractGenericExceptionHandler>>(serviceProvider => key =>
            {
                return GetExceptionHandler(serviceProvider, key);
            });
            #endregion

        }
    }
}
