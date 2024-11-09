using Generic.Base.Handler.Map;
using PMIS.DTO.Claim.Info;
using PMIS.DTO.Claim;
using PMIS.DTO.User.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.User
{
    public class UserSearchResponseDto:UserStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
           where TDestination : class
           where TSource : class
        {
            if (source is Models.User sourceModel)
            {
                if (destination is UserSearchResponseDto destinationModel)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<UserStandardInfoDto, UserSearchResponseDto>(await destinationModel.extraMapFromBaseModel(sourceModel));
                }

            }
            return destination;
        }
    }
}
