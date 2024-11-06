using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Service.DTO
{
    public class GenericSearchRequestDto
    {
        public List<GenericSearchFilterDto> filters { get; set; } = new List<GenericSearchFilterDto>();
        public List<GenericSearchSortDto> sorts { get; set; } = new List<GenericSearchSortDto>();
        public int? recordCount { get; set; } = null;
        public int? pageNumber { get; set; } = null;

    }
    public class GenericSearchResponseDto
    {
    }
}
