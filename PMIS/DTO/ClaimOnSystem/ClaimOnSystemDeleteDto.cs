using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimOnSystem
{
    
    public class ClaimOnSystemDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class ClaimOnSystemDeleteResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
