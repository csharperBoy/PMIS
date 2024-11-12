using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User
{
    
    public class UserDeleteRequestDto : GenericDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class UserDeleteResponseDto : GenericDeleteResponseDto
    {
        public int Id { get; set; }
    }
}
