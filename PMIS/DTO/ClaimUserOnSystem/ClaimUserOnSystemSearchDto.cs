using Generic.Base.Handler.Map;
using PMIS.DTO.ClaimUserOnIndicator.Info;
using PMIS.DTO.ClaimUserOnIndicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;
using PMIS.DTO.ClaimUserOnSystem.Info;

namespace PMIS.DTO.ClaimUserOnSystem
{
    public class ClaimUserOnSystemSearchResponseDto : ClaimUserOnSystemStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is Models.ClaimUserOnSystem sourceModel)
            {
                if (destination is ClaimUserOnSystemSearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<ClaimUserOnSystemStandardInfoDto, ClaimUserOnSystemSearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }
}
