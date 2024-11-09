using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.Claim;
using PMIS.DTO.IndicatorCategory.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.IndicatorCategory
{
    public class IndicatorCategorySearchResponseDto:IndicatorCategoryStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            if (source is Models.IndicatorCategory sourceModel)
            {
                if (destination is IndicatorCategorySearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<IndicatorCategoryStandardInfoDto, IndicatorCategorySearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }
}
