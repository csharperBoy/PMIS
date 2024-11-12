using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Indicator
{
    public class IndicatorDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class IndicatorDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
