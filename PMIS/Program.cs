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
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.Forms;
using PMIS.Models;
using PMIS.Services;
using PMIS.Services.Contract;
using Serilog;
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
            
            ConfigureServicesProvider(serviceProvider);

            //var loginForm = serviceProvider.GetRequiredService<LoginForm>();
            //Application.Run(loginForm); 
            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
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

            #region LookUp
            services.AddTransient<AbstractGenericRepository<LookUp, PmisContext>, GenericSqlServerRepository<LookUp, PmisContext>>();
            services.AddTransient<ILookUpService, LookUpService>();
            services.AddTransient<AbstractGenericNormalService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto, LookUpEditRequestDto, LookUpEditResponseDto, LookUpDeleteRequestDto, LookUpDeleteResponseDto, LookUpSearchResponseDto>, GenericNormalService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto, LookUpEditRequestDto, LookUpEditResponseDto, LookUpDeleteRequestDto, LookUpDeleteResponseDto, LookUpSearchResponseDto>>();
            services.AddTransient<AbstractGenericNormalAddService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto>, GenericNormalAddService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto>>();
            services.AddTransient<AbstractGenericNormalEditService<PmisContext, LookUp, LookUpEditRequestDto, LookUpEditResponseDto>, GenericNormalEditService<PmisContext, LookUp, LookUpEditRequestDto, LookUpEditResponseDto>>();
            services.AddTransient<AbstractGenericNormalLogicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto>>();
            services.AddTransient<AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto>>();
            services.AddTransient<AbstractGenericNormalSearchService<PmisContext, LookUp, LookUpSearchResponseDto>, GenericNormalSearchService<PmisContext, LookUp, LookUpSearchResponseDto>>();
            #endregion
         
            #region LookUpValue
            services.AddTransient<AbstractGenericRepository<LookUpValue, PmisContext>, GenericSqlServerRepository<LookUpValue, PmisContext>>();
            services.AddTransient<ILookUpValueService, LookUpValueService>();
            services.AddTransient<AbstractGenericNormalService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>, GenericNormalService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>>();
            services.AddTransient<AbstractGenericNormalAddService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto>, GenericNormalAddService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto>>();
            services.AddTransient<AbstractGenericNormalEditService<PmisContext, LookUpValue, LookUpValueEditRequestDto, LookUpValueEditResponseDto>, GenericNormalEditService<PmisContext, LookUpValue, LookUpValueEditRequestDto, LookUpValueEditResponseDto>>();
            services.AddTransient<AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto>>();
            services.AddTransient<AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto>>();
            services.AddTransient<AbstractGenericNormalSearchService<PmisContext, LookUpValue, LookUpValueSearchResponseDto>, GenericNormalSearchService<PmisContext, LookUpValue, LookUpValueSearchResponseDto>>();
            #endregion

            #region LookUpDestination
            services.AddTransient<AbstractGenericRepository<LookUpDestination, PmisContext>, GenericSqlServerRepository<LookUpDestination, PmisContext>>();
            services.AddTransient<ILookUpDestinationService, LookUpDestinationService>();
            services.AddTransient<AbstractGenericNormalService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto, LookUpDestinationSearchResponseDto>, GenericNormalService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto, LookUpDestinationSearchResponseDto>>();
            services.AddTransient<AbstractGenericNormalAddService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto>, GenericNormalAddService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto>>();
            services.AddTransient<AbstractGenericNormalEditService<PmisContext, LookUpDestination, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto>, GenericNormalEditService<PmisContext, LookUpDestination, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto>>();
            services.AddTransient<AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto>>();
            services.AddTransient<AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto>>();
            services.AddTransient<AbstractGenericNormalSearchService<PmisContext, LookUpDestination, LookUpDestinationSearchResponseDto>, GenericNormalSearchService<PmisContext, LookUpDestination, LookUpDestinationSearchResponseDto>>();
            #endregion

            #region Indicator
            services.AddTransient<AbstractGenericRepository<Indicator, PmisContext>, GenericSqlServerRepository<Indicator, PmisContext>>();
            services.AddTransient<IIndicatorService, IndicatorService>();
            services.AddTransient<AbstractGenericNormalService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchResponseDto>, GenericNormalService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchResponseDto>>();
            services.AddTransient<AbstractGenericNormalAddService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto>, GenericNormalAddService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto>>();
            services.AddTransient<AbstractGenericNormalEditService<PmisContext, Indicator, IndicatorEditRequestDto, IndicatorEditResponseDto>, GenericNormalEditService<PmisContext, Indicator, IndicatorEditRequestDto, IndicatorEditResponseDto>>();
            services.AddTransient<AbstractGenericNormalLogicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto>>();
            services.AddTransient<AbstractGenericNormalPhysicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto>>();
            services.AddTransient<AbstractGenericNormalSearchService<PmisContext, Indicator, IndicatorSearchResponseDto>, GenericNormalSearchService<PmisContext, Indicator, IndicatorSearchResponseDto>>();
            #endregion

        }
        private static void ConfigureServicesProvider(IServiceProvider serviceProvider)
        {
            var logHandler = serviceProvider.GetRequiredService<AbstractGenericLogWithSerilogHandler>().CreateLogger();
            var exceptionHandler = serviceProvider.GetRequiredService<AbstractGenericExceptionHandler>();
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                exceptionHandler.HandleException(e.ExceptionObject as Exception);
                logHandler.Error(e.ExceptionObject as Exception, "An unhandled exception occurred.");
            };
            Application.ThreadException += (sender, e) =>
            {
                exceptionHandler.HandleException(e.Exception);
                logHandler.Error(e.Exception, "A thread exception occurred.");
            };
        }
        private static void ConfigureGenericServicesContainer(IServiceCollection services)
        {
            #region Log
            GenericConfigureLogWithSerilogRequestDto reqCustomizeLog = new GenericConfigureLogWithSerilogRequestDto()
            {
                minimumLevel = Serilog.Events.LogEventLevel.Debug,
                logHandlerType = GenericLogWithSerilogHandlerFactory.LogHandlerType.Database,
                rollingInterval = Serilog.RollingInterval.Day,
                inSqlServerConfig = new GenericConfigureLogWithSerilogInSqlServerRequestDto()
                {
                    connectionString = configuration.GetSection("LogConfig:ConnectionString").Value,
                    tableName = configuration.GetSection("LogConfig:TableName").Value,
                }
            };
            GenericConfiguration.ConfigureGenericLogServices(services, reqCustomizeLog);

            GenericConfigureLogWithSerilogRequestDto reqGlobalLog = new GenericConfigureLogWithSerilogRequestDto()
            {
                minimumLevel = Serilog.Events.LogEventLevel.Information,
                logHandlerType = GenericLogWithSerilogHandlerFactory.LogHandlerType.File,
                rollingInterval = Serilog.RollingInterval.Day,
                inFileConfig = new GenericConfigureLogWithSerilogInFileRequestDto()
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), configuration.GetSection("LogConfig:FolderName").Value, configuration.GetSection("LogConfig:FileName").Value)
                }
            };
            Log.Logger = GenericLogWithSerilogHandlerFactory.GetLogHandler(reqGlobalLog).CreateLogger();
            #endregion

            GenericConfiguration.ConfigureGenericMapServices(services);
            GenericConfiguration.ConfigureGenericSystemExceptionServices(services);
        }
    }
}