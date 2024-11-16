using Generic.Base.Handler.Map;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Repository;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.Indicator;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.ClaimUserOnIndicator
{
    
    public class ClaimUserOnIndicatorDeleteRequestDto : GenericDeleteRequestDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
          where TDestination : class
          where TSource : class
        {
            GenericSqlServerRepository<Models.ClaimUserOnIndicator, PmisContext> repository = new GenericSqlServerRepository<Models.ClaimUserOnIndicator, PmisContext>(new PmisContext(),
                new GenericExceptionHandler()
                ,
                new GenericLogWithSerilogInFileHandler(
                            new Generic.Base.Handler.SystemLog.WithSerilog.DTO.GenericConfigureLogWithSerilogRequestDto()
                            {
                                inFileConfig = new Generic.Base.Handler.SystemLog.WithSerilog.DTO.GenericConfigureLogWithSerilogInFileRequestDto()
                                { filePath = "C:\\Users\\868\\source\\repos\\PMIS\\PMIS\\bin\\Debug\\net8.0-windows\\logs\\log20241108.txt" },
                                logHandlerType = GenericLogWithSerilogHandlerFactory.LogHandlerType.File,
                                minimumLevel = Serilog.Events.LogEventLevel.Information,
                                rollingInterval = Serilog.RollingInterval.Hour,
                            }
                        )
                );
            if (source is ClaimUserOnIndicatorDeleteRequestDto sourceModel)
            {
                if (destination is Models.ClaimUserOnIndicator destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.ClaimUserOnIndicator, Models.ClaimUserOnIndicator>(await repository.GetByIdAsync(sourceModel.Id), destinationModel);
                    //                    destinationModel =await re.GetByIdAsync(sourceModel.Id);
                }

            }
            return destination;
        }
        public int Id { get; set; }
    }
    public class ClaimUserOnIndicatorDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
