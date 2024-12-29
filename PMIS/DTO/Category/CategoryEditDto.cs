using Generic.Base.Handler.Map;
using Generic.Base.Handler.SystemException.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog.Concrete;
using Generic.Base.Handler.SystemLog.WithSerilog;
using Generic.Repository;
using Generic.Service.DTO.Concrete;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.Category
{
    public class CategoryEditRequestDto : GenericEditRequestDto
    {
        public static async Task<TDestination> BeforeMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            GenericSqlServerRepository<Models.Category, PmisContext> repository = new GenericSqlServerRepository<Models.Category, PmisContext>(new PmisContext(),
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
            if (source is CategoryEditRequestDto sourceModel)
            {
                if (destination is Models.Category destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.Category, Models.Category>(await repository.GetByIdAsync(sourceModel.Id), destinationModel);
                }

            }
            return destination;
        }
        public int Id { get; set; }

        public int FkLkpCategoryTypeId { get; set; }

        public int? FkParentId { get; set; }

        public string? Code { get; set; }

        public string? Title { get; set; }

        public byte? OrderNum { get; set; }

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        //public bool FlgLogicalDelete { get; set; }
    }
    public class CategoryEditResponseDto : GenericEditResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            if (source is Models.Category sourceModel)
            {
                if (destination is CategoryEditResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }
    }
}
