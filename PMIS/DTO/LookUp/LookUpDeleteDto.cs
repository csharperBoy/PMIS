using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUp
{
    public class LookUpDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class LookUpDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
