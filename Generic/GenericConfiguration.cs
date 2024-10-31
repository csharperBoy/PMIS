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
using Serilog;
using Microsoft.Extensions.Logging;
using Generic.Service.ShapeExample;

namespace Generic
{
    public static class GenericConfiguration
    {
        public static void ConfigureGenericServices(IServiceCollection services)
        {
            #region Log
            services.AddSingleton<AbstractGenericLogHandler, GenericLogInFileHandler>();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders(); 
                loggingBuilder.AddSerilog(dispose: true);
            });
            var logger = ServiceProvider.GetRequiredService<ILogger<Program>>();

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

        public static void ConfigureSerilogServices(IServiceCollection services)
        {
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs", "log.txt");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });


            services.AddSingleton<Func<MappingMode, AbstractGenericMapHandler>>(serviceProvider => key =>
            {
                return GetMapper(serviceProvider, key);
            });
        }
    }
}
