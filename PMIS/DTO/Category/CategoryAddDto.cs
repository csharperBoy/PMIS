using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Category
{
    public class CategoryAddRequestDto : GenericAddRequestDto
    {
       // public int Id { get; set; }

        public string? Title { get; set; }

        public int? FkParentId { get; set; }

        public string? CategoryCode { get; set; }

        public byte? OrderCategory { get; set; }

        public int FklkpTypeId { get; set; }

        public string? Description { get; set; }

        public string? SystemInfo { get; set; }

        //public bool FlgLogicalDelete { get; set; }

        //public virtual Category? FkParent { get; set; }

        //public virtual LookUpValue FklkpType { get; set; } = null!;

        //public virtual ICollection<IndicatorCategory> IndicatorCategories { get; set; } = new List<IndicatorCategory>();

        //public virtual ICollection<Category> InverseFkParent { get; set; } = new List<Category>();
    }
    public class CategoryAddResponseDto : GenericAddResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
          where TDestination : class
          where TSource : class
        {
            if (source is Models.Category sourceModel)
            {
                if (destination is CategoryAddResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }

    }
}
