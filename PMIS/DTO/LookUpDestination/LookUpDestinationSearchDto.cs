using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Generic.Base.Handler.Map;
using Generic.Helper;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination.Info;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Models;
using static Generic.Base.Handler.Map.GenericMapHandlerFactory;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationSearchResponseDto: LookUpDestinationStandardInfoDto
    {
        public static async Task<TDestination> AfterMap<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (source is Models.LookUpDestination sourceModel1)
            {
                if (destination is LookUpDestinationSearchResponseDto destinationModel1)
                {
                     await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<LookUpDestinationStandardInfoDto, LookUpDestinationSearchResponseDto>(await destinationModel1.extraMapFromBaseModel(sourceModel1));                   
                }

            }
            else if(source is LookUpDestinationSearchResponseDto sourceModel2)
            {
                if (destination is Models.LookUpDestination destinationModel2)
                {
                    await GenericMapHandlerFactory.GetMapper(MappingMode.Auto).Map<Models.LookUpDestination, LookUpDestinationSearchResponseDto>(await sourceModel2.extraMapToBaseModel(sourceModel2));
                }
            }
            return destination;
        }

    }


}
