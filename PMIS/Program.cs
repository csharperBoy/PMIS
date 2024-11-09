﻿using Generic;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Abstract;
using Generic.Base.Handler.Map.Concrete;
using Generic.Base.Handler.SystemException.Abstract;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Base.Handler.SystemLog.WithSerilog.Abstract;
using Generic.Base.Handler.SystemLog.WithSerilog.DTO;
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
using PMIS.DTO.Claim;
using PMIS.DTO.Indicator;
using PMIS.DTO.IndicatorCategory;
using PMIS.DTO.IndicatorValue;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.User;
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

            #region Claim
            services.AddScoped<AbstractGenericRepository<Claim, PmisContext>, GenericSqlServerRepository<Claim, PmisContext>>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<AbstractGenericNormalService<PmisContext, Claim, ClaimAddRequestDto, ClaimAddResponseDto, ClaimEditRequestDto, ClaimEditResponseDto, ClaimDeleteRequestDto, ClaimDeleteResponseDto, ClaimSearchResponseDto>, GenericNormalService<PmisContext, Claim, ClaimAddRequestDto, ClaimAddResponseDto, ClaimEditRequestDto, ClaimEditResponseDto, ClaimDeleteRequestDto, ClaimDeleteResponseDto, ClaimSearchResponseDto>>();
            services.AddScoped<AbstractGenericNormalAddService<PmisContext, Claim, ClaimAddRequestDto, ClaimAddResponseDto>, GenericNormalAddService<PmisContext, Claim, ClaimAddRequestDto, ClaimAddResponseDto>>();
            services.AddScoped<AbstractGenericNormalEditService<PmisContext, Claim, ClaimEditRequestDto, ClaimEditResponseDto>, GenericNormalEditService<PmisContext, Claim, ClaimEditRequestDto, ClaimEditResponseDto>>();
            services.AddScoped<AbstractGenericNormalLogicalDeleteService<PmisContext, Claim, ClaimDeleteRequestDto, ClaimDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, Claim, ClaimDeleteRequestDto, ClaimDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalPhysicalDeleteService<PmisContext, Claim, ClaimDeleteRequestDto, ClaimDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, Claim, ClaimDeleteRequestDto, ClaimDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalSearchService<PmisContext, Claim, ClaimSearchResponseDto>, GenericNormalSearchService<PmisContext, Claim, ClaimSearchResponseDto>>();
            #endregion

            #region Indicator
            services.AddScoped<AbstractGenericRepository<Indicator, PmisContext>, GenericSqlServerRepository<Indicator, PmisContext>>();
            services.AddScoped<IIndicatorService, IndicatorService>();
            services.AddScoped<AbstractGenericNormalService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchResponseDto>, GenericNormalService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto, IndicatorEditRequestDto, IndicatorEditResponseDto, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto, IndicatorSearchResponseDto>>();
            services.AddScoped<AbstractGenericNormalAddService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto>, GenericNormalAddService<PmisContext, Indicator, IndicatorAddRequestDto, IndicatorAddResponseDto>>();
            services.AddScoped<AbstractGenericNormalEditService<PmisContext, Indicator, IndicatorEditRequestDto, IndicatorEditResponseDto>, GenericNormalEditService<PmisContext, Indicator, IndicatorEditRequestDto, IndicatorEditResponseDto>>();
            services.AddScoped<AbstractGenericNormalLogicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalPhysicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, Indicator, IndicatorDeleteRequestDto, IndicatorDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalSearchService<PmisContext, Indicator, IndicatorSearchResponseDto>, GenericNormalSearchService<PmisContext, Indicator, IndicatorSearchResponseDto>>();
            #endregion

            #region IndicatorCategory
            services.AddScoped<AbstractGenericRepository<IndicatorCategory, PmisContext>, GenericSqlServerRepository<IndicatorCategory, PmisContext>>();
            services.AddScoped<IIndicatorCategoryService, IndicatorCategoryService>();
            services.AddScoped<AbstractGenericNormalService<PmisContext, IndicatorCategory, IndicatorCategoryAddRequestDto, IndicatorCategoryAddResponseDto, IndicatorCategoryEditRequestDto, IndicatorCategoryEditResponseDto, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto, IndicatorCategorySearchResponseDto>, GenericNormalService<PmisContext, IndicatorCategory, IndicatorCategoryAddRequestDto, IndicatorCategoryAddResponseDto, IndicatorCategoryEditRequestDto, IndicatorCategoryEditResponseDto, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto, IndicatorCategorySearchResponseDto>>();
            services.AddScoped<AbstractGenericNormalAddService<PmisContext, IndicatorCategory, IndicatorCategoryAddRequestDto, IndicatorCategoryAddResponseDto>, GenericNormalAddService<PmisContext, IndicatorCategory, IndicatorCategoryAddRequestDto, IndicatorCategoryAddResponseDto>>();
            services.AddScoped<AbstractGenericNormalEditService<PmisContext, IndicatorCategory, IndicatorCategoryEditRequestDto, IndicatorCategoryEditResponseDto>, GenericNormalEditService<PmisContext, IndicatorCategory, IndicatorCategoryEditRequestDto, IndicatorCategoryEditResponseDto>>();
            services.AddScoped<AbstractGenericNormalLogicalDeleteService<PmisContext, IndicatorCategory, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, IndicatorCategory, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalPhysicalDeleteService<PmisContext, IndicatorCategory, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, IndicatorCategory, IndicatorCategoryDeleteRequestDto, IndicatorCategoryDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalSearchService<PmisContext, IndicatorCategory, IndicatorCategorySearchResponseDto>, GenericNormalSearchService<PmisContext, IndicatorCategory, IndicatorCategorySearchResponseDto>>();
            #endregion

            #region IndicatorValue
            services.AddScoped<AbstractGenericRepository<IndicatorValue, PmisContext>, GenericSqlServerRepository<IndicatorValue, PmisContext>>();
            services.AddScoped<IIndicatorValueService, IndicatorValueService>();
            services.AddScoped<AbstractGenericNormalService<PmisContext, IndicatorValue, IndicatorValueAddRequestDto, IndicatorValueAddResponseDto, IndicatorValueEditRequestDto, IndicatorValueEditResponseDto, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto, IndicatorValueSearchResponseDto>, GenericNormalService<PmisContext, IndicatorValue, IndicatorValueAddRequestDto, IndicatorValueAddResponseDto, IndicatorValueEditRequestDto, IndicatorValueEditResponseDto, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto, IndicatorValueSearchResponseDto>>();
            services.AddScoped<AbstractGenericNormalAddService<PmisContext, IndicatorValue, IndicatorValueAddRequestDto, IndicatorValueAddResponseDto>, GenericNormalAddService<PmisContext, IndicatorValue, IndicatorValueAddRequestDto, IndicatorValueAddResponseDto>>();
            services.AddScoped<AbstractGenericNormalEditService<PmisContext, IndicatorValue, IndicatorValueEditRequestDto, IndicatorValueEditResponseDto>, GenericNormalEditService<PmisContext, IndicatorValue, IndicatorValueEditRequestDto, IndicatorValueEditResponseDto>>();
            services.AddScoped<AbstractGenericNormalLogicalDeleteService<PmisContext, IndicatorValue, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, IndicatorValue, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalPhysicalDeleteService<PmisContext, IndicatorValue, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, IndicatorValue, IndicatorValueDeleteRequestDto, IndicatorValueDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalSearchService<PmisContext, IndicatorValue, IndicatorValueSearchResponseDto>, GenericNormalSearchService<PmisContext, IndicatorValue, IndicatorValueSearchResponseDto>>();
            #endregion

            #region LookUp
            services.AddScoped<AbstractGenericRepository<LookUp, PmisContext>, GenericSqlServerRepository<LookUp, PmisContext>>();
            services.AddScoped<ILookUpService, LookUpService>();
            services.AddScoped<AbstractGenericNormalService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto, LookUpEditRequestDto, LookUpEditResponseDto, LookUpDeleteRequestDto, LookUpDeleteResponseDto, LookUpSearchResponseDto>, GenericNormalService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto, LookUpEditRequestDto, LookUpEditResponseDto, LookUpDeleteRequestDto, LookUpDeleteResponseDto, LookUpSearchResponseDto>>();
            services.AddScoped<AbstractGenericNormalAddService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto>, GenericNormalAddService<PmisContext, LookUp, LookUpAddRequestDto, LookUpAddResponseDto>>();
            services.AddScoped<AbstractGenericNormalEditService<PmisContext, LookUp, LookUpEditRequestDto, LookUpEditResponseDto>, GenericNormalEditService<PmisContext, LookUp, LookUpEditRequestDto, LookUpEditResponseDto>>();
            services.AddScoped<AbstractGenericNormalLogicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, LookUp, LookUpDeleteRequestDto, LookUpDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalSearchService<PmisContext, LookUp, LookUpSearchResponseDto>, GenericNormalSearchService<PmisContext, LookUp, LookUpSearchResponseDto>>();
            #endregion

            #region LookUpDestination
            services.AddScoped<AbstractGenericRepository<LookUpDestination, PmisContext>, GenericSqlServerRepository<LookUpDestination, PmisContext>>();
            services.AddScoped<ILookUpDestinationService, LookUpDestinationService>();
            services.AddScoped<AbstractGenericNormalService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto, LookUpDestinationSearchResponseDto>, GenericNormalService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto, LookUpDestinationSearchResponseDto>>();
            services.AddScoped<AbstractGenericNormalAddService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto>, GenericNormalAddService<PmisContext, LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto>>();
            services.AddScoped<AbstractGenericNormalEditService<PmisContext, LookUpDestination, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto>, GenericNormalEditService<PmisContext, LookUpDestination, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto>>();
            services.AddScoped<AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, LookUpDestination, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalSearchService<PmisContext, LookUpDestination, LookUpDestinationSearchResponseDto>, GenericNormalSearchService<PmisContext, LookUpDestination, LookUpDestinationSearchResponseDto>>();
            #endregion

            #region LookUpValue
            services.AddScoped<AbstractGenericRepository<LookUpValue, PmisContext>, GenericSqlServerRepository<LookUpValue, PmisContext>>();
            services.AddScoped<ILookUpValueService, LookUpValueService>();
            services.AddScoped<AbstractGenericNormalService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>, GenericNormalService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>>();
            services.AddScoped<AbstractGenericNormalAddService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto>, GenericNormalAddService<PmisContext, LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto>>();
            services.AddScoped<AbstractGenericNormalEditService<PmisContext, LookUpValue, LookUpValueEditRequestDto, LookUpValueEditResponseDto>, GenericNormalEditService<PmisContext, LookUpValue, LookUpValueEditRequestDto, LookUpValueEditResponseDto>>();
            services.AddScoped<AbstractGenericNormalLogicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalPhysicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, LookUpValue, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalSearchService<PmisContext, LookUpValue, LookUpValueSearchResponseDto>, GenericNormalSearchService<PmisContext, LookUpValue, LookUpValueSearchResponseDto>>();
            #endregion
            
            #region User
            services.AddScoped<AbstractGenericRepository<User, PmisContext>, GenericSqlServerRepository<User, PmisContext>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<AbstractGenericNormalService<PmisContext, User, UserAddRequestDto, UserAddResponseDto, UserEditRequestDto, UserEditResponseDto, UserDeleteRequestDto, UserDeleteResponseDto, UserSearchResponseDto>, GenericNormalService<PmisContext, User, UserAddRequestDto, UserAddResponseDto, UserEditRequestDto, UserEditResponseDto, UserDeleteRequestDto, UserDeleteResponseDto, UserSearchResponseDto>>();
            services.AddScoped<AbstractGenericNormalAddService<PmisContext, User, UserAddRequestDto, UserAddResponseDto>, GenericNormalAddService<PmisContext, User, UserAddRequestDto, UserAddResponseDto>>();
            services.AddScoped<AbstractGenericNormalEditService<PmisContext, User, UserEditRequestDto, UserEditResponseDto>, GenericNormalEditService<PmisContext, User, UserEditRequestDto, UserEditResponseDto>>();
            services.AddScoped<AbstractGenericNormalLogicalDeleteService<PmisContext, User, UserDeleteRequestDto, UserDeleteResponseDto>, GenericNormalLogicalDeleteService<PmisContext, User, UserDeleteRequestDto, UserDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalPhysicalDeleteService<PmisContext, User, UserDeleteRequestDto, UserDeleteResponseDto>, GenericNormalPhysicalDeleteService<PmisContext, User, UserDeleteRequestDto, UserDeleteResponseDto>>();
            services.AddScoped<AbstractGenericNormalSearchService<PmisContext, User, UserSearchResponseDto>, GenericNormalSearchService<PmisContext, User, UserSearchResponseDto>>();
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