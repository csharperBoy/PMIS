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

namespace PMIS.DTO.ClaimUserOnSystem
{
    public class ClaimUserOnSystemEditRequestDto : GenericEditRequestDto
    {
        public static async Task<TDestination> BeforeMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            GenericSqlServerRepository<Models.ClaimUserOnSystem, PmisContext> repository = new GenericSqlServerRepository<Models.ClaimUserOnSystem, PmisContext>(new PmisContext(),
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
            if (source is ClaimUserOnSystemEditRequestDto sourceModel)
            {
                if (destination is Models.ClaimUserOnSystem destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.ClaimUserOnSystem, Models.ClaimUserOnSystem>(await repository.GetByIdAsync(sourceModel.Id), destinationModel);
                }

            }
            return destination;
        }
        public int Id { get; set; }

        public int FkLkpClaimUserOnSystemId { get; set; }

        public int FkUserId { get; set; }

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        //public virtual LookUpValue FkLkpClaimOnSystem { get; set; } = null!;

        //public virtual User FkUser { get; set; } = null!;
    }
    public class ClaimUserOnSystemEditResponseDto : GenericEditResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            if (source is Models.ClaimUserOnSystem sourceModel)
            {
                if (destination is ClaimUserOnSystemEditResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }
    }
}
