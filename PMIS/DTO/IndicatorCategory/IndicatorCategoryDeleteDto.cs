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

namespace PMIS.DTO.IndicatorCategory
{
    
    public class IndicatorCategoryDeleteRequestDto : GenericDeleteRequestDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
        where TDestination : class
        where TSource : class
        {
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            GenericSqlServerRepository<Models.IndicatorCategory, PmisContext> repository = new GenericSqlServerRepository<Models.IndicatorCategory, PmisContext>(new PmisContext(),
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
            if (source is IndicatorCategoryDeleteRequestDto sourceModel)
            {
                if (destination is Models.IndicatorCategory destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.IndicatorCategory, Models.IndicatorCategory>(await repository.GetByIdAsync(sourceModel.Id), destinationModel);
                    //                    destinationModel =await re.GetByIdAsync(sourceModel.Id);
                }

            }
            return destination;
        }

        public int Id { get; set; }
    }
    public class IndicatorCategoryDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
