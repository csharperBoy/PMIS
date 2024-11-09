using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User
{
       public class UserEditDto
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

        //public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

        //public virtual LookUpValue FkLkpWorkCalenar { get; set; } = null!;
    }

    public class UserEditResponseDto
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
