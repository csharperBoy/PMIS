using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorCategory
{
    
    public class IndicatorCategoryDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class IndicatorCategoryDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
