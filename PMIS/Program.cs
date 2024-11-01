using Generic;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.DTO.Base.Handler.SystemLog.Serilog;
using Generic.Repository;
using Generic.Repository.Abstract;
using Generic.Service.Normal.Composition;
using Generic.Service.Normal.Composition.Abstract;
using Generic.Service.Normal.Operation;
using Generic.Service.Normal.Operation.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PMIS.DTO.Indicator;
using PMIS.Forms;
using PMIS.Models;
using PMIS.Services;
using PMIS.Services.Contract;
using System.Configuration;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;
namespace PMIS
{
    internal static class Program
    {
        public static bool useLazyLoad { get; set; } = true;
        public static IConfigurationRoot configuration { get; set; } = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
        public static ServiceProvider serviceProvider { get; private set; }
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var serviceCollection = new ServiceCollection();
            ConfigureGenericServicesContainer(serviceCollection);

            ConfigureServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();
            
            ConfigureGenericServicesProvider(serviceProvider);

            var loginForm = serviceProvider.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            #region Other
            //var configuration = new ConfigurationBuilder()
            //   .SetBasePath(Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json")
            //   .Build();
            #endregion

            #region Database
            services.AddDbContext<PmisContext>(options =>
            {
                options.UseSqlServer(configuration.GetSection("ConnectionStrings:PmisConnectionString").Value);
                options.UseLazyLoadingProxies(useLazyLoad);
                options.EnableSensitiveDataLogging(true);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient);
            #endregion

            #region Forms
            services.AddTransient<MainForm>();
            services.AddTransient<LoginForm>();
            #endregion

            #region Indicator
            services.AddTransient<AbstractGenericRepository<Indicator, PmisContext>, GenericSqlServerRepository<Indicator, PmisContext>>();
            services.AddTransient<IIndicatorService, IndicatorService>();
            services.AddTransient<AbstractGenericNormalService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto>, GenericNormalService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto>>();
            services.AddTransient<AbstractGenericNormalAddService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto>, GenericNormalAddService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto>>();
            services.AddTransient<AbstractGenericNormalEditService<PmisContext, Indicator, IndicatorEditRequestDto, IndicatorEditResponseDto>, GenericNormalEditService<PmisContext, Indicator, IndicatorEditRequestDto, IndicatorEditResponseDto>>();
            #endregion

        }
        private static void ConfigureGenericServicesProvider(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<AbstractGenericLogWithSerilogHandler>().CreateLogger();
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                logger.Error(e.ExceptionObject as Exception, "An unhandled exception occurred.");
            };

            Application.ThreadException += (sender, e) =>
            {
                logger.Error(e.Exception, "A thread exception occurred.");
            };
        }
      
        private static void ConfigureGenericServicesContainer(IServiceCollection services)
        {
            #region Log
            GenericConfigureLogWithSerilogRequestDto req = new GenericConfigureLogWithSerilogRequestDto()
            {
                minimumLevel = Serilog.Events.LogEventLevel.Debug,
                logHandlerType = GenericLogWithSerilogHandlerFactory.LogHandlerType.File,
                rollingInterval = Serilog.RollingInterval.Day,
                inFileConfig = new GenericConfigureLogWithSerilogInFileRequestDto()
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), configuration.GetSection("LogConfig:FolderName").Value, configuration.GetSection("LogConfig:FileName").Value)
                },
                inSqlServerConfig = new GenericConfigureLogWithSerilogInSqlServerRequestDto()
                {
                    connectionString = configuration.GetSection("LogConfig:ConnectionString").Value,
                    tableName = configuration.GetSection("LogConfig:TableName").Value,
                }
            };
            GenericConfiguration.ConfigureGenericLogServices(services,req);
            //var logHandler = GenericConfiguration.ConfigureGenericLogServices(services,);
            //Serilog.Log.Logger = logHandler.CreateLogger();
            #endregion

            GenericConfiguration.ConfigureGenericMapServices(services);
            GenericConfiguration.ConfigureGenericSystemExceptionServices(services);
        }
    }
}