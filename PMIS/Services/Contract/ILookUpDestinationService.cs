using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.LookUp;
using PMIS.DTO.LookUpDestination;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface ILookUpDestinationService : IGenericNormalService<LookUpDestination, LookUpDestinationAddRequestDto, LookUpDestinationAddResponseDto, LookUpDestinationEditRequestDto, LookUpDestinationEditResponseDto, LookUpDestinationDeleteRequestDto, LookUpDestinationDeleteResponseDto, LookUpDestinationSearchResponseDto>
    {
    }
}
