using AutoMapper;
using Generic.Base.Handler.Map.Contract;
using PMIS.DTO.LookUpDestination.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.LookUpDestination
{
    public class LookUpDestinationAddRequestDto
    {
        public static async Task<TDestination> AfterMap<TSource,TDestination>(TSource source, TDestination destination)
            where TDestination : class
            where TSource : class
        {
            if (destination is Models.LookUpDestination lookUpDestinationDestination)
            {
                if (source is LookUpDestinationAddRequestDto addRequesSource)
                {
                    lookUpDestinationDestination.SystemInfo = DateTime.Now.ToString();
                }
                else if (source is LookUpDestinationEditRequestDto editRequesSource)
                {
                    lookUpDestinationDestination.SystemInfo = DateTime.Now.ToString();
                }
            }
            else if (source is Models.LookUpDestination LookUpSource)
            {
                if (destination is LookUpDestinationAddResponseDto addResponsDestination)
                {
                    //addResponsDestination.ErrorMessage = $"{LookUpSource.Code} {LookUpSource.Title}";
                }
                else if (destination is LookUpDestinationEditResponseDto editResponsDestination)
                {
                    //editResponsDestination.ErrorMessage = $"{LookUpSource.Code} {LookUpSource.Title}";
                }
                else if (destination is LookUpDestinationSearchResponseDto searchResponsDestination)
                {
                    //var a = searchResponsDestination.extraMapFromBaseModel(LookUpSource) ;
                    //if(a is LookUpDestinationStandardInfoDto)
                    //{
                    //    var b = a as LookUpDestinationSearchResponseDto;
                    //    destination = b as TDestination;
                    //}
                    // destination = (searchResponsDestination.extraMapFromBaseModel(LookUpSource) as LookUpDestinationSearchResponseDto) as TDestination;            

                    //searchResponsDestination = await mapper.Map<LookUpDestinationStandardInfoDto, LookUpDestinationSearchResponseDto>(searchResponsDestination.extraMapFromBaseModel(LookUpSource));
                }
            }
            else
            {

            }
            return destination;
        }
        // public int Id { get; set; }

        public int FkLookUpId { get; set; }

        public string TableName { get; set; } = null!;

        public string ColumnName { get; set; } = null!;

        public string? Description { get; set; }

        public string? SystemInfo { get; set; }

        public bool? FlgLogicalDelete { get; set; }

      
    }
    public class LookUpDestinationAddResponseDto 
    {
        public static async Task<LookUpDestinationAddResponseDto> AfterMap(Models.LookUpDestination src , LookUpDestinationAddResponseDto dest)
        {
            dest.ErrorMessage = "test des";          
            return dest;
        }

      
        public string ErrorMessage { get; set; }
    }
}
