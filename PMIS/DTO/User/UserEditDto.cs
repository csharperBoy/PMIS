using Generic.Service.DTO.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User
{
       public class UserEditRequestDto : GenericEditRequestDto
    {
         public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public int FkLkpWorkCalenarId { get; set; }

        public string? Phone { get; set; }

        public string? Description { get; set; }

        //public string? SystemInfo { get; set; }

        //public bool? FlgLogicalDelete { get; set; }

        //public virtual ICollection<ClaimUserOnIndicator> ClaimUserOnIndicators { get; set; } = new List<ClaimUserOnIndicator>();

        //public virtual LookUpValue FkLkpWorkCalenar { get; set; } = null!;
    }

    public class UserEditResponseDto : GenericEditResponseDto
    {
        public int Id { get; set; }
    }
}
