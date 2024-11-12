using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimUserOnIndicator
{
    
    public class ClaimUserOnIndicatorDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class ClaimUserOnIndicatorDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
