using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnIndicator.Info;
using PMIS.DTO.Indicator.Info;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpDestination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.ClaimUserOnIndicator
{
    public class ClaimUserOnIndicatorSearchResponseDto : ClaimUserOnIndicatorStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is Models.ClaimUserOnIndicator sourceModel)
            {
                if (destination is ClaimUserOnIndicatorSearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<ClaimUserOnIndicatorStandardInfoDto, ClaimUserOnIndicatorSearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }
}
