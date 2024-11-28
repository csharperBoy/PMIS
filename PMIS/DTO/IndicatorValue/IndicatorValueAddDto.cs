using Generic.Helper;
using Generic.Service.DTO.Concrete;
using PMIS.DTO.IndicatorCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorValue
{
    public class IndicatorValueAddRequestDto : GenericAddRequestDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
        where TDestination : class
        where TSource : class
        {
            
            if (source is IndicatorValueAddRequestDto sourceModel2)
            {
                if (destination is Models.IndicatorValue destinationModel2)
                {
                    destinationModel2.DateTime = Helper.Convert.ConvertShamsiToGregorian(sourceModel2.shamsiDateTime).GetValueOrDefault();
                }
            }
            return destination;
        }
        //  public long Id { get; set; }

        public int FkIndicatorId { get; set; }

        public int FkLkpValueTypeId { get; set; }

        public int FkLkpShiftId { get; set; }

        public DateTime DateTime { get; set; }

        public string shamsiDateTime { get; set; }
        public decimal Value { get; set; }

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        //public virtual Indicator FkIndicator { get; set; } = null!;

        //public virtual LookUpValue FkLkpShift { get; set; } = null!;

        //public virtual LookUpValue FkLkpValueType { get; set; } = null!;
    }
    public class IndicatorValueAddResponseDto : GenericAddResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
         where TDestination : class
         where TSource : class
        {
            if (source is Models.IndicatorValue sourceModel)
            {
                if (destination is IndicatorValueAddResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
           
            return destination;
        }
        public int Id { get; set; }

    }
}
