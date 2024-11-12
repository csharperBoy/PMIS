using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpValue
{
    public class LookUpValueDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class LookUpValueDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
