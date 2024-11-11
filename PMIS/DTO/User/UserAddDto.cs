using PMIS.DTO.LookUpValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User
{
    public class UserAddRequestDto
    {
       // public int Id { get; set; }

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
    public class UserAddResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
         where TDestination : class
         where TSource : class
        {
            if (source is Models.User sourceModel)
            {
                if (destination is UserAddResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
