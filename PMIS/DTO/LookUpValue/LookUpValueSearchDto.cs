using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMIS.DTO.LookUp;
using PMIS.Models;

namespace PMIS.DTO.LookUpValue
{
    public class LookUpValueSearchResponseDto
    {
        public LookUpValueSearchResponseDto()
        {
                
        }
        //public LookUpValueSearchResponseDto(PMIS.Models.LookUpValue baseModel)
        //{
        //    this.TableName = baseModel.LookUpValueDestinations.FirstOrDefault().TableName;
        //    this.ColumnName = baseModel.LookUpValueDestinations.FirstOrDefault().ColumnName;
        //    this.Code = baseModel.Code;
        //    this.Title = baseModel.Title;
        //}
        public LookUpValueSearchResponseDto extraMapFromBaseModel(PMIS.Models.LookUpValue baseModel)
        {
            this.FkLookUp = (new LookUpSearchResponseDto()).extraMapFromBaseModel(baseModel.FkLookUp);
            return this;
        }
        public int Id { get; set; }

        //public int FkLookUpId { get; set; }

        public string Value { get; set; } = null!;

        public string Display { get; set; } = null!;

        public int OrderNum { get; set; }

        public string? Description { get; set; }

       // public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

      //  public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

        public virtual LookUpSearchResponseDto FkLookUp { get; set; } = null!;

       // public virtual ICollection<IndicatorCategory> IndicatorCategoryFkLkpCategoryDetails { get; set; } = new List<IndicatorCategory>();

//        public virtual ICollection<IndicatorCategory> IndicatorCategoryFkLkpCategoryMasters { get; set; } = new List<IndicatorCategory>();

//        public virtual ICollection<IndicatorCategory> IndicatorCategoryFkLkpCategoryTypes { get; set; } = new List<IndicatorCategory>();

  //      public virtual ICollection<Indicator> IndicatorFkLkpDesirabilities { get; set; } = new List<Indicator>();

    //    public virtual ICollection<Indicator> IndicatorFkLkpForms { get; set; } = new List<Indicator>();

      //  public virtual ICollection<Indicator> IndicatorFkLkpManualities { get; set; } = new List<Indicator>();

        //public virtual ICollection<Indicator> IndicatorFkLkpMeasures { get; set; } = new List<Indicator>();

  //      public virtual ICollection<Indicator> IndicatorFkLkpPeriods { get; set; } = new List<Indicator>();

  //      public virtual ICollection<Indicator> IndicatorFkLkpUnits { get; set; } = new List<Indicator>();

    //    public virtual ICollection<IndicatorValue> IndicatorValueFkLkpShifts { get; set; } = new List<IndicatorValue>();

//        public virtual ICollection<IndicatorValue> IndicatorValueFkLkpValueTypes { get; set; } = new List<IndicatorValue>();

  //      public virtual ICollection<User> Users { get; set; } = new List<User>();
    }

 
}
