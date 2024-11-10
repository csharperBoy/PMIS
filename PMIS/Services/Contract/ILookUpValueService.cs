using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
using PMIS.DTO.LookUpValue;
using PMIS.DTO.LookUpValue.Info;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface ILookUpValueService : IGenericNormalService<LookUpValue, LookUpValueAddRequestDto, LookUpValueAddResponseDto, LookUpValueEditRequestDto, LookUpValueEditResponseDto, LookUpValueDeleteRequestDto, LookUpValueDeleteResponseDto, LookUpValueSearchResponseDto>
    {
        Task<IEnumerable<LookUpDestinationSearchResponseDto>> GetList(string _tableName);
        Task<IEnumerable<LookUpValueShortInfoDto>> GetList(string _tableName, string _columnName, string _code);
        Task<IEnumerable<LookUpValueShortInfoDto>> GetList(IEnumerable<LookUpDestinationSearchResponseDto> _tablelookUpList, string _columnName, string _code);
    }
}
