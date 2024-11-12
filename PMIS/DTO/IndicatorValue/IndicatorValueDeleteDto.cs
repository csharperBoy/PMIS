using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorValue
{
    
    public class IndicatorValueDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class IndicatorValueDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
