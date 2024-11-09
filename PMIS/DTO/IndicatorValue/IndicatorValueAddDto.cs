using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorValue
{
    public class IndicatorValueAddDto
    {
      //  public long Id { get; set; }

        public int FkIndicatorId { get; set; }

        public int FkLkpValueTypeId { get; set; }

        public int FkLkpShiftId { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Value { get; set; }

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        //public virtual Indicator FkIndicator { get; set; } = null!;

        //public virtual LookUpValue FkLkpShift { get; set; } = null!;

        //public virtual LookUpValue FkLkpValueType { get; set; } = null!;
    }
    public class IndicatorValueAddResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
