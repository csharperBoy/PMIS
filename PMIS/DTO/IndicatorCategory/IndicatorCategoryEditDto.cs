using Generic.Base.Handler.Map;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Repository;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.ClaimUserOnIndicator;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.IndicatorCategory
{
      public class IndicatorCategoryEditRequestDto : GenericEditRequestDto
    {
        public static async Task<TDestination> BeforeMap<TSource, TDestination>(TSource source, TDestination destination)
        where TDestination : class
        where TSource : class
        {
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
            if (source is IndicatorCategoryEditRequestDto sourceModel)
            {
                if (destination is Models.IndicatorCategory destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.IndicatorCategory, Models.IndicatorCategory>(await repository.GetByIdAsync(sourceModel.Id), destinationModel);
                }

            }
            return destination;
        }

        public int Id { get; set; }

        public int FkIndicatorId { get; set; }

        public int FkLkpCategoryTypeId { get; set; }

        public int FkLkpCategoryMasterId { get; set; }

        public int FkLkpCategoryDetailId { get; set; }

        public int OrderNum { get; set; }

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        //public virtual Indicator FkIndicator { get; set; } = null!;

        //public virtual LookUpValue FkLkpCategoryDetail { get; set; } = null!;

        //public virtual LookUpValue FkLkpCategoryMaster { get; set; } = null!;

        //public virtual LookUpValue FkLkpCategoryType { get; set; } = null!;
    }

    public class IndicatorCategoryEditResponseDto : GenericEditResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
         where TDestination : class
         where TSource : class
        {
            if (source is Models.IndicatorCategory sourceModel)
            {
                if (destination is IndicatorCategoryEditResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }

        public int Id { get; set; }
    }
}
