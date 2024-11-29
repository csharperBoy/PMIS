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
    public class ClaimUserOnIndicatorEditRequestDto : GenericEditRequestDto
    {
        public static async Task<TDestination> BeforeMap<TSource, TDestination>(TSource source, TDestination destination)
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
                               { filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs\\log.txt") },
                               logHandlerType = GenericLogWithSerilogHandlerFactory.LogHandlerType.File,
                               minimumLevel = Serilog.Events.LogEventLevel.Information,
                               rollingInterval = Serilog.RollingInterval.Hour,
                           }
                       )
               );
            if (source is ClaimUserOnIndicatorEditRequestDto sourceModel)
            {
                if (destination is Models.ClaimUserOnIndicator destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.ClaimUserOnIndicator, Models.ClaimUserOnIndicator>(await repository.GetByIdAsync(sourceModel.Id), destinationModel);
                }

            }
            return destination;
        }
        public int Id { get; set; }

        public int FkLkpClaimUserOnIndicatorId { get; set; }

        public int FkUserId { get; set; }

        public int FkIndicatorId { get; set; }

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        //public virtual Indicator FkIndicator { get; set; } = null!;

        //public virtual LookUpValue FkLkpClaimUserOnIndicator { get; set; } = null!;

        //public virtual User FkUser { get; set; } = null!;
    }
    public class ClaimUserOnIndicatorEditResponseDto : GenericEditResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            if (source is Models.ClaimUserOnIndicator sourceModel)
            {
                if (destination is ClaimUserOnIndicatorEditResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }
    }
}
