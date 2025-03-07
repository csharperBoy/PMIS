﻿using Generic.Base.Handler.Map;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Repository;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.Models;
using PMIS.Repository;
using PMIS.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.Indicator
{
    public class IndicatorDeleteRequestDto : GenericDeleteRequestDto
    {
       

        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            GenericSqlServerRepository<Models.Indicator, PmisContext> repository = new GenericSqlServerRepository<Models.Indicator, PmisContext>(new PmisContext() ,
                new GenericExceptionHandler()
                ,
                new GenericLogWithSerilogInFileHandler(
                            new Generic.Base.Handler.SystemLog.WithSerilog.DTO.GenericConfigureLogWithSerilogRequestDto()
                            {
                                inFileConfig = new Generic.Base.Handler.SystemLog.WithSerilog.DTO.GenericConfigureLogWithSerilogInFileRequestDto()
                                { filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs\\log.txt")},
                                logHandlerType = GenericLogWithSerilogHandlerFactory.LogHandlerType.File,
                                minimumLevel = Serilog.Events.LogEventLevel.Information,
                                rollingInterval = Serilog.RollingInterval.Hour,
                            }
                        )
                );
            if (source is IndicatorDeleteRequestDto sourceModel)
            {
                if (destination is Models.Indicator destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.Indicator, Models.Indicator>(await repository.GetByIdAsync(sourceModel.Id) , destinationModel);
//                    destinationModel =await re.GetByIdAsync(sourceModel.Id);
                }

            }
            return destination;
        }
        public int Id { get; set; }
    }
    public class IndicatorDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
