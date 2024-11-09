using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.Claim;
using PMIS.DTO.Indicator.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.Indicator
{
   
    public class IndicatorSearchResponseDto : IndicatorStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is Models.Indicator sourceModel)
            {
                if (destination is IndicatorSearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<IndicatorStandardInfoDto, IndicatorSearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }
}
