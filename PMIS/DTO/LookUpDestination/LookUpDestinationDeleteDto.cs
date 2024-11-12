using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class LookUpDestinationDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
