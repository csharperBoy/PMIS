using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimOnSystem
{
    
    public class ClaimOnSystemDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class ClaimOnSystemDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
