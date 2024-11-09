using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User
{
    
    public class UserDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class UserDeleteResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
