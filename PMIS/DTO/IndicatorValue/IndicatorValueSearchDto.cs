using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.Claim;
using PMIS.DTO.IndicatorValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.IndicatorValue
{
    public class IndicatorValueSearchResponseDto : IndicatorValueStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            if (source is Models.IndicatorValue sourceModel)
            {
                if (destination is IndicatorValueSearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<IndicatorValueStandardInfoDto, IndicatorValueSearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }
}
