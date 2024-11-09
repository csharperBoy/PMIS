using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.Claim;
using PMIS.DTO.LookUp.Info;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Models;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.LookUp
{
    public class LookUpSearchResponseDto: LookUpStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is Models.LookUp sourceModel)
            {
                if (destination is LookUpSearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<LookUpStandardInfoDto, LookUpSearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }

  
}
