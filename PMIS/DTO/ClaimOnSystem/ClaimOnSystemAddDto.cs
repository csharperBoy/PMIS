using Generic.Service.DTO.Concrete;
using PMIS.DTO.Indicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.ClaimOnSystem
{
    public class ClaimOnSystemAddRequestDto : GenericAddRequestDto
    {
       // public int Id { get; set; }

        public int? FkLkpClaimOnSystemId { get; set; }

        public int? FkUserId { get; set; }

        public string? Description { get; set; }

      //  public string? SystemInfo { get; set; }

      //  public bool? FlgLogicalDelete { get; set; }

      //  public virtual LookUpValue? FkLkpClaimOnSystem { get; set; }

      //  public virtual User? FkUser { get; set; }
    }
    public class ClaimOnSystemAddResponseDto : GenericAddResponseDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            if (source is Models.ClaimOnSystem sourceModel)
            {
                if (destination is ClaimOnSystemAddResponseDto destinationModel)
                {
                    destinationModel.IsSuccess = sourceModel.Id != 0 ? true : false;
                }
            }
            return destination;
        }

        public int Id { get; set; }

    }
}
