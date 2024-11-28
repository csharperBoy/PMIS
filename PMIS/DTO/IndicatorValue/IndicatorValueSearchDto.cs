using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnIndicator.Info;
using PMIS.DTO.ClaimUserOnIndicator;
using PMIS.DTO.IndicatorValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;
using Generic.Helper;

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
                    destinationModel.shamsiDateTime = Helper.Convert.ConvertGregorianToShamsi(sourceModel.DateTime, "RRRR/MM/DD");
                }

            }
            return destination;
        }
    }
}
