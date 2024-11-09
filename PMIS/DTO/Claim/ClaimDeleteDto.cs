using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.Claim
{
    
    public class ClaimDeleteRequestDto
    {
        public int Id { get; set; }
    }
    public class ClaimDeleteResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
