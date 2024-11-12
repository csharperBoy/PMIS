using Generic.Service.DTO.Concrete;
using PMIS.DTO.IndicatorCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUp
{
    public class LookUpAddRequestDto : GenericAddRequestDto
    {

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

    }
    public class LookUpAddResponseDto : GenericAddResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
          where TDestination : class
          where TSource : class
        {
            if (source is Models.LookUp sourceModel)
            {
                if (destination is LookUpAddResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }
    }
}
