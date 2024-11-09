using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpDestination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.Claim
{
    public class ClaimSearchResponseDto : ClaimStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is Models.Claim sourceModel)
            {
                if (destination is ClaimSearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<ClaimStandardInfoDto, ClaimSearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }
}
