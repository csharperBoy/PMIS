using Generic.Service.DTO.Concrete;
using PMIS.DTO.ClaimUserOnSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorCategory
{
    public class IndicatorCategoryAddRequestDto : GenericAddRequestDto
    {
      //  public int Id { get; set; }

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
    public class IndicatorCategoryAddResponseDto : GenericAddResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
          where TDestination : class
          where TSource : class
        {
            if (source is Models.IndicatorCategory sourceModel)
            {
                if (destination is IndicatorCategoryAddResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }

    }
}
