using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic.Base.Handler.Map;
using Generic.Base.Handler.Map.Concrete;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.Claim;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Models;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.LookUpValue
{
    public class LookUpValueSearchResponseDto: LookUpValueStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
             where TDestination : class
             where TSource : class
        {
            if (source is Models.LookUpValue sourceModel)
            {
                if (destination is LookUpValueSearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<LookUpValueStandardInfoDto, LookUpValueSearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }


}
