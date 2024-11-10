using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimOnSystem
{
    public class ClaimOnSystemEditRequestDto
    {
         public int Id { get; set; }

        public int? FkLkpClaimOnSystemId { get; set; }

        public int? FkUserId { get; set; }

        public string? Description { get; set; }

        //  public string? SystemInfo { get; set; }

        //  public bool? FlgLogicalDelete { get; set; }

        //  public virtual LookUpValue? FkLkpClaimOnSystem { get; set; }

        //  public virtual User? FkUser { get; set; }
    }

    public class ClaimOnSystemEditResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
