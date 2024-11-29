using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Repository;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.LookUpDestination;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Base.Handler.Map;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.Indicator
{
    public class IndicatorEditRequestDto : GenericEditRequestDto
    {
        public static async Task<TDestination> BeforeMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            GenericSqlServerRepository<Models.Indicator, PmisContext> repository = new GenericSqlServerRepository<Models.Indicator, PmisContext>(new PmisContext(),
               new GenericExceptionHandler()
               ,
               new GenericLogWithSerilogInFileHandler(
                           new Generic.Base.Handler.SystemLog.WithSerilog.DTO.GenericConfigureLogWithSerilogRequestDto()
                           {
                               inFileConfig = new Generic.Base.Handler.SystemLog.WithSerilog.DTO.GenericConfigureLogWithSerilogInFileRequestDto()
                               { filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs\\log.txt") },
                               logHandlerType = GenericLogWithSerilogHandlerFactory.LogHandlerType.File,
                               minimumLevel = Serilog.Events.LogEventLevel.Information,
                               rollingInterval = Serilog.RollingInterval.Hour,
                           }
                       )
               );
            if (source is IndicatorEditRequestDto sourceModel)
            {
                if (destination is Models.Indicator destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.Indicator, Models.Indicator>(await repository.GetByIdAsync(sourceModel.Id), destinationModel);                                                      
                }

            }
            return destination;
        }
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int FkLkpFormId { get; set; }

        public int FkLkpManualityId { get; set; }

        public int FkLkpUnitId { get; set; }

        public int FkLkpPeriodId { get; set; }

        public int FkLkpMeasureId { get; set; }

        public int FkLkpDesirabilityId { get; set; }

        public string? Formula { get; set; }

        public string? Description { get; set; }

        public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }
    }
    public class IndicatorEditResponseDto : GenericEditResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            if (source is Models.Indicator sourceModel)
            {
                if (destination is IndicatorEditResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }
    }
}
