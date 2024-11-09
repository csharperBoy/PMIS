using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.IndicatorCategory
{
    
    public class IndicatorCategoryDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class IndicatorCategoryDeleteResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
